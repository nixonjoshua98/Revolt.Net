using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Extensions;
using Revolt.Net.Rest.Extensions;

namespace Revolt.Net.Commands.Handlers
{
    internal sealed class CommandMessageHandler(
        ICommandProcessor _commandProcessor
    ) : ICommandMessageHandler
    {
        public async Task HandleAsync(CommandContext context, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(context.Message.Content))
            {
                return;
            }

            if (!context.Message.HasStringPrefix("!", out var idx))
            {
                return;
            }

            var result = await _commandProcessor.ExecuteAsync(context, idx, cancellationToken);

            if (result.Error is not null)
            {
                await context.Message.ReplyAsync(result.ErrorMessage, cancellationToken: cancellationToken);
            }
        }
    }
}
