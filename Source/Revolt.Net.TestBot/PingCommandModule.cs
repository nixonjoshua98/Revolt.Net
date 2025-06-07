using Revolt.Net.Commands;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Attributes;
using Revolt.Net.Rest;

namespace Revolt.Net.TestBot
{
    internal sealed class PingCommandModule(RevoltApiClient _apiClient) : CommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext context)
        {
            await _apiClient.SendMessageAsync(context.Message.ChannelId, "Pong");
        }
    }
}