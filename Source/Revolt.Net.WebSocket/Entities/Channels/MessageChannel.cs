using Revolt.Net.WebSocket.Helpers;

namespace Revolt.Net.WebSocket
{
    public class MessageChannel : Channel
    {
        public async Task<ClientMessage> SendMessageAsync(string content = null, Embed embed = null, IEnumerable<Embed> embeds = null) =>
            await MessageHelper.SendAsync(
                client: Client,
                channelId: Id,
                content: content,
                embed: embed,
                embeds : embeds
            );

        public async Task<SocketMessage> GetMessageAsync(string messageId)
        {
            return await Client.State.Messages.GetAsync(Id, messageId);
        }
    }
}