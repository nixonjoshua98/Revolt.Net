using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Exceptions;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.TypeBinding;
using Revolt.Net.Core.Exceptions;
using System.Reflection;

namespace Revolt.Net.Commands.Services
{
    internal sealed class CommandProcessor(
        RevoltCommandState _commandState,
        ILogger<CommandProcessor> _logger,
        IServiceProvider _serviceProvider
    ) : ICommandProcessor
    {
        public async Task<CommandExecutionResult> ExecuteAsync(
            CommandContext context,
            int commandStartIndex,
            CancellationToken cancellationToken)
        {
            try
            {
                var (commandName, commandArguments) = ParseCommandArguments(context, commandStartIndex);

                var isExecuted = await ExecuteSingleCommandAsync(context, commandName, commandArguments, cancellationToken);

                if (!isExecuted)
                {
                    return CommandExecutionResult.AsError(CommandExecutionError.CommandNotFound, "Command not found");
                }

                return CommandExecutionResult.AsSuccess();
            }
            catch (CommandParameterBindException binding)
            {
                return CommandExecutionResult.AsError(CommandExecutionError.BindingFailed, binding.Message);
            }
            catch (Exception ex)
            {
                return CommandExecutionResult.AsError(CommandExecutionError.Exception, ex.Message);
            }
        }

        private (string CommandName, List<string> CommandArguments) ParseCommandArguments(CommandContext context, int commandStartIndex)
        {
            var arguments = context.GetArguments(commandStartIndex);

            if (arguments.Count == 0)
            {
                throw new RevoltException("Command require at least one argument to be processable.");
            }

            var commandName = arguments[0];
            var commandArguments = arguments.Count > 1 ? arguments[1..] : [];

            return (commandName, commandArguments);
        }

        private async Task<bool> ExecuteSingleCommandAsync(CommandContext context, string commandName, List<string> commandArguments, CancellationToken cancellationToken)
        {
            var defaultValues = CreateDefaultParameters(context, cancellationToken);

            List<InvokableCommand> invokableCommands = [];

            foreach (var command in _commandState.GetCommands(commandName))
            {
                var invokableCommand = await GetInvokableCommandAsync(
                    context,
                    command,
                    commandArguments,
                    defaultValues,
                    cancellationToken
                );

                if (invokableCommand is not null)
                {
                    invokableCommands.Add(invokableCommand);
                }
            }

            return await TryInvokeCommandAsync(invokableCommands, cancellationToken);
        }

        private async Task<bool> TryInvokeCommandAsync(
            List<InvokableCommand> invokableCommands,
            CancellationToken cancellationToken)
        {
            if (invokableCommands.Count == 0)
            {
                return false;
            }

            var invokable = invokableCommands[0];

            if (invokableCommands.Count > 1)
            {
                invokable = invokableCommands.MaxBy(x => x.RegisteredCommand.Priority)!;

                var invokableSamePriorityCmd = invokableCommands
                    .Count(x => x.RegisteredCommand.Priority == invokable.RegisteredCommand.Priority);

                if (invokableSamePriorityCmd > 1)
                {
                    _logger.LogWarning("Revolt.Net : Found {CommandCount} invokable commands with the same priority, first one was picked to be executed.", invokableSamePriorityCmd);
                }
            }

            await ExecuteInvokableCommandAsync(invokable, cancellationToken);

            return true;
        }

        private async Task<InvokableCommand?> GetInvokableCommandAsync(
            CommandContext context,
            RegisteredCommand command,
            List<string> arguments,
            Dictionary<Type, object> defaultValues,
            CancellationToken cancellationToken)
        {
            var parameterValues = new object?[command.Parameters.Count];

            int inpParamIndex = 0;

            foreach (var (index, parameter) in command.Parameters.Index())
            {
                if (defaultValues.TryGetValue(parameter.ParameterType, out var defaultValue))
                {
                    parameterValues[index] = defaultValue;

                    continue;
                }

                // The parameter is out of bounds fom the input text + the parameter has no default
                // making it required. It's not possible to use this command.
                else if (inpParamIndex >= arguments.Count && !parameter.HasDefaultValue)
                {
                    return null;
                }

                // ---

                if (inpParamIndex >= arguments.Count)
                {
                    parameterValues[index] = parameter.DefaultValue!;

                    continue;
                }

                var rawValue = arguments[inpParamIndex];

                var parseResult = await ParseAsync(context, rawValue, parameter, cancellationToken);

                if (parseResult.Status == TypeParseStatus.Success)
                {
                    parameterValues[index] = parseResult.Value;
                }
                else if (parseResult.Status == TypeParseStatus.Failed)
                {
                    if (parameter.HasDefaultValue)
                    {
                        parameterValues[index] = parameter.DefaultValue;
                    }

                    // Parsing failed, and the parameter has no default so the command is unusable.
                    else
                    {
                        return null;
                    }
                }

                inpParamIndex++;
            }

            return new InvokableCommand(command, parameterValues);
        }

        private async Task ExecuteInvokableCommandAsync(InvokableCommand command, CancellationToken cancellationToken)
        {
            var moduleInstance = ActivatorUtilities.CreateInstance(_serviceProvider, command.RegisteredCommand.ModuleType);

            var methodReturn = command.RegisteredCommand.CommandMethod.Invoke(moduleInstance, command.Parameters.Length == 0 ? null : command.Parameters);

            switch (methodReturn)
            {
                case Task task:
                    await task.WaitAsync(cancellationToken);
                    break;

                case ValueTask valueTask:
                    await valueTask;
                    break;
            }
        }

        private async Task<TypeParseResult> ParseAsync(
            CommandContext context,
            string rawValue,
            ParameterInfo parameter,
            CancellationToken cancellationToken)
        {
            if (parameter.ParameterType == typeof(string))
            {
                return TypeParseResult.Success(rawValue);
            }
            else
            {
                var binderContext = new TypeParserContext(context, parameter.ParameterType, rawValue);

                var binderMeta = _commandState.GetTypeBinder(parameter.ParameterType);

                var binderInstance = _serviceProvider.GetService(binderMeta.BinderType);

                Task<TypeParseResult>? parseTask = null;

                if (binderInstance is not null)
                {
                    var invokeResult = binderMeta.MethodInfo.Invoke(binderInstance, [binderContext, cancellationToken])!;

                    parseTask = invokeResult as Task<TypeParseResult>;
                }

                // Non-type targetted type parsers
                foreach (var parser in _serviceProvider.GetServices<ITypeParser>())
                {
                    var canParse = await parser.CanParseAsync(binderContext, cancellationToken);

                    if (canParse)
                    {
                        parseTask = parser.ParseAsync(binderContext, cancellationToken);

                        break;
                    }
                }

                return await (parseTask ?? throw CommandParameterBindException.NoRegisteredType(parameter.ParameterType));
            }
        }

        private Dictionary<Type, object> CreateDefaultParameters(CommandContext context, CancellationToken cancellationToken)
        {
            return new Dictionary<Type, object>()
            {
                [typeof(CommandContext)] = context,
                [typeof(CancellationToken)] = cancellationToken
            };
        }

    }
}