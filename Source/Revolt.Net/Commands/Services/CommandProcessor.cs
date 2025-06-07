using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Configuration;
using Revolt.Net.Commands.Models;

namespace Revolt.Net.Commands.Services
{
    internal sealed class CommandProcessor(
        IOptions<RevoltCommandState> _commandStateOptions,
        IServiceProvider _serviceProvider
    ) : ICommandProcessor
    {
        private readonly RevoltCommandState _commandState = _commandStateOptions.Value;

        public async Task ExecuteAsync(CommandContext context, int startIndex, CancellationToken cancellationToken)
        {
            var commandName = context.Message.Content![startIndex..];

            if (_commandState.TryGetCommandByName(commandName, out var command))
            {
                await ExecuteAsync(context, command, cancellationToken);
            }
        }

        private async Task ExecuteAsync(CommandContext context, RegisteredCommand command, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var moduleInstance = ActivatorUtilities.CreateInstance(
                scope.ServiceProvider,
                command.ModuleType
            );

            var parameters = command.CommandMethod.GetParameters();

            var arguments = parameters
                .Select(p =>
                {
                    if (p.ParameterType == typeof(CommandContext))
                    {
                        return context;
                    }

                    return null;
                })
                .Where(x => x is not null)
                .ToArray();

            var methodReturn = command.CommandMethod.Invoke(moduleInstance, arguments.Length == 0 ? null : arguments);

            if (methodReturn is Task returnTask)
            {
                await returnTask;
            }
        }
    }
}