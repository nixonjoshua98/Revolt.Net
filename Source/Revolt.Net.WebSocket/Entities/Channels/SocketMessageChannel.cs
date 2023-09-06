using Revolt.Net.WebSocket.Helpers;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket
{
    public abstract class SocketMessageChannel : SocketChannel
    {
        public async Task<SocketClientMessage> SendMessageAsync(string content = null, Embed embed = null, IEnumerable<Embed> embeds = null) =>
            await ChannelHelper.SendMessageAsync(
                client: Client,
                channelId: Id,
                content: content,
                embed: embed,
                embeds : embeds
            );

        public async ValueTask<SocketMessage> GetMessageAsync(string messageId, FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await ChannelHelper.GetMessageAsync(Client, Id, messageId, behaviour);
    }
}