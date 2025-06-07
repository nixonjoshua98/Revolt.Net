using Revolt.Net.Commands.Abstractions;

namespace Revolt.Net.Commands.Handlers
{
    internal sealed class CommandMessageHandler(
        ICommandProcessor _commandProcessor
    ) : ICommandMessageHandler
    {
        public async Task HandleAsync(CommandContext context, CancellationToken cancellationToken)
        {
            await _commandProcessor.ExecuteAsync(context, 0, cancellationToken);
        }
    }
}
