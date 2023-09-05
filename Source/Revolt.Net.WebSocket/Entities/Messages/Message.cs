using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class Message : SocketEntity
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
        public Channel Channel => Client.GetChannel(ChannelId);

        [JsonIgnore]
        public User Author => Client.GetUser(AuthorId);

        public async Task<ClientMessage> ReplyAsync(string content) =>
            await Client.State.Messages.SendAsync(ChannelId, Id, content);

        public async Task DeleteAsync() =>
            await Client.State.Messages.DeleteAsync(ChannelId, Id);

        public async Task AcknowledgeAsync() =>
            await Client.Api.AcknowledgeMessageAsync(ChannelId, Id);
    }
}
