using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Exceptions;
using Revolt.Net.Commands.Models;
using Revolt.Net.Commands.Parsers;
using Revolt.Net.Core.Exceptions;
using System.Reflection;

namespace Revolt.Net.Commands.Services
{
    internal sealed class CommandProcessor(
        RevoltCommandsState _commandState,
        ILogger<CommandProcessor> _logger,
        IServiceProvider _serviceProvider
    ) : IRevoltCommandProcessor
    {
        public async Task<CommandExecutionResult> ExecuteAsync(CommandContext context, int commandStartIndex, CancellationToken cancellationToken)
        {
            try
            {
                var (commandName, commandArguments) = ParseCommandArguments(context, commandStartIndex);

                return await ExecuteSingleCommandAsync(context, commandName, commandArguments, cancellationToken);
            }
            catch (CommandParameterBindException binding)
            {
                return CommandExecutionResult.Failure(CommandExecutionError.BindingFailed, binding.Message);
            }
            catch (Exception ex)
            {
                return CommandExecutionResult.Failure(CommandExecutionError.Exception, ex.Message);
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

        private async Task<CommandExecutionResult> ExecuteSingleCommandAsync(CommandContext context, string commandName, List<string> commandArguments, CancellationToken cancellationToken)
        {
            var defaultValues = CreateDefaultParameters(context, cancellationToken);

            List<InvokableCommand> invokableCommands = [];

            CommandExecutionResult? result = null;

            foreach (var command in _commandState.GetCommands(commandName))
            {
                var invokableCommand = await GetInvokableCommandAsync(
                    context,
                    command,
                    commandArguments,
                    defaultValues,
                    cancellationToken
                );

                if (invokableCommand is null)
                {
                    result = CommandExecutionResult.Failure(CommandExecutionError.BindingFailed, "Command found, but failed to bind arguments to command");

                    continue;
                }

                result = await PerformCheckAsync(context, invokableCommand, cancellationToken);

                if (result.HasValue && result.Value.IsSuccess)
                {
                    invokableCommands.Add(invokableCommand);
                }
            }

            return invokableCommands.Count switch
            {
                0 when result is null => CommandExecutionResult.Failure(CommandExecutionError.CommandNotFound, "Command not found"),
                0 when result is not null => result.Value,
                _ => await TryInvokeCommandAsync(invokableCommands, cancellationToken)
            };
        }

        private async Task<CommandExecutionResult> PerformCheckAsync(CommandContext context, InvokableCommand command, CancellationToken cancellationToken)
        {
            foreach (var (descriptor, data) in _commandState.GetPreCheckDescriptors(command.CommandDescriptor))
            {
                var result = await descriptor.CheckAsync(context, data, _serviceProvider, cancellationToken);

                if (!result.IsSuccess)
                {
                    return CommandExecutionResult.Failure(CommandExecutionError.PreCheckFailed, result.Message ?? "A command pre-check failed");
                }
            }

            return CommandExecutionResult.Success();
        }

        private async Task<CommandExecutionResult> TryInvokeCommandAsync(List<InvokableCommand> invokableCommands, CancellationToken cancellationToken)
        {
            var invokable = invokableCommands[0];

            if (invokableCommands.Count > 1)
            {
                invokable = invokableCommands.MaxBy(x => x.CommandDescriptor.Priority)!;

                var invokableSamePriorityCmd = invokableCommands
                    .Count(x => x.CommandDescriptor.Priority == invokable.CommandDescriptor.Priority);

                if (invokableSamePriorityCmd > 1)
                {
                    _logger.LogWarning("Found {CommandCount} invokable commands with the same priority, first one was picked to be executed.", invokableSamePriorityCmd);
                }
            }

            return await ExecuteInvokableCommandAsync(invokable, cancellationToken);
        }

        private async Task<InvokableCommand?> GetInvokableCommandAsync(
            CommandContext context,
            RevoltCommandDescriptor command,
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

        private async Task<CommandExecutionResult> ExecuteInvokableCommandAsync(InvokableCommand command, CancellationToken cancellationToken)
        {
            var moduleInstance = ActivatorUtilities.CreateInstance(_serviceProvider, command.CommandDescriptor.ModuleType);

            var methodReturn = command.CommandDescriptor.CommandMethod.Invoke(moduleInstance, command.Parameters.Length == 0 ? null : command.Parameters);

            switch (methodReturn)
            {
                case Task task:
                    await task.WaitAsync(cancellationToken);
                    break;

                case ValueTask valueTask:
                    await valueTask;
                    break;
            }

            return CommandExecutionResult.Success();
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

                // Generic parser
                if (_commandState.TryGetGenericTypeParser(parameter.ParameterType, _serviceProvider, out var result))
                {
                    var invokeResult = result.Data.ParseMethod.Invoke(result.ParserInstance, [binderContext, cancellationToken])!;

                    return await (invokeResult as Task<TypeParseResult> ?? throw CommandParameterBindException.NoRegisteredType(parameter.ParameterType));
                }

                // Non-type targetted type parsers
                foreach (var parser in _commandState.GetTypelessHandlers(_serviceProvider))
                {
                    var canParse = await parser.CanParseAsync(binderContext, cancellationToken);

                    if (canParse)
                    {
                        return await parser.ParseAsync(binderContext, cancellationToken);
                    }
                }

                throw CommandParameterBindException.NoRegisteredType(parameter.ParameterType);
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