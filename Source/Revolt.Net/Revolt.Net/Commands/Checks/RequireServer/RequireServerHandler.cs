using Revolt.Net.Core.Entities.Messages;

namespace Revolt.Net.Commands.Checks.RequireServer
{
    public sealed class RequireServerHandler : ICommandPreCheckHandler<RequireServerAttribute>
    {
        public Task<CommandPreCheckResult> CheckAsync(CommandContext context, RequireServerAttribute check, CancellationToken cancellationToken)
        {
            var result = context.Message is ServerMessage message && message.ServerId == check.ServerId ?
                CommandPreCheckResult.Success() :
                CommandPreCheckResult.Failed("Command is not accessable from this server");

            return Task.FromResult(result);
        }
    }
}
