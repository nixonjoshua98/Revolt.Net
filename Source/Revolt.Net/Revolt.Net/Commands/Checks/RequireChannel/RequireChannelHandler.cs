using Revolt.Net.Core.Common;

namespace Revolt.Net.Commands.Checks.RequireChannel
{
    public sealed class RequireChannelHandler : ICommandPreCheckHandler<RequireChannelAttribute>
    {
        public Task<CommandPreCheckResult> CheckAsync(CommandContext context, RequireChannelAttribute check, CancellationToken cancellationToken)
        {
            var result = context.Message.ChannelId == check.ChannelId ?
                CommandPreCheckResult.Success() :
                CommandPreCheckResult.Failed($"Command is only accessable from channel {RevoltFormatter.Channel(check.ChannelId)}");

            return Task.FromResult(result);
        }
    }
}
