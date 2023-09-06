using Revolt.Net.WebSocket.Helpers;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class SocketMessage : SocketEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        public string Nonce { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; }

        [JsonPropertyName("author")]
        public string AuthorId { get; init; }

        public string Content { get; init; }

        [JsonIgnore]
        public SocketChannel Channel => Client.GetChannel(ChannelId);

        [JsonIgnore]
        public IUser Author => Client.GetUser(AuthorId);

        public async Task<SocketClientMessage> ReplyAsync(string content, Embed embed = null, IEnumerable<Embed> embeds = null,  bool mention = false) =>
            await ChannelHelper.SendMessageAsync(
                client: Client,
                channelId: ChannelId,
                content: content,
                messageId: Id,
                mention: mention,
                embed: embed,
                embeds: embeds
            );

        public async Task DeleteAsync() =>
            await ChannelHelper.DeleteMessageAsync(Client, ChannelId, Id);

        public async Task AcknowledgeAsync() =>
            await Client.Api.AcknowledgeMessageAsync(ChannelId, Id);

        public async Task RemoveReactionsAsync() =>
            await Client.Api.RemoveMessageReactionsAsync(ChannelId, Id);
    }
}
