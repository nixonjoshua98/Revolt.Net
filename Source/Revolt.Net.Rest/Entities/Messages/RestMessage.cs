using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Helpers;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public class RestMessage : RestEntity, IMessage
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; }

        [JsonPropertyName("author")]
        public string AuthorId { get; init; }

        public string Content { get; init; }

        [JsonIgnore]
        public ITextChannel Channel { get; private set; }

        [JsonIgnore]
        public IUser Author { get; private set; }

        internal void Update(RevoltClientBase client, ITextChannel channel, IUser author)
        {
            Client = client;
            Channel = channel;
            Author = author;
        }

        public async Task<IClientMessage> ReplyAsync(string content, Embed embed = null, IEnumerable<Embed> embeds = null, bool mention = false) =>
            await ChannelHelper.SendMessageAsync(
                client: Client,
                channel: Channel,
                content: content,
                messageId: Id,
                mention: mention,
                embed: embed,
                embeds: embeds
            );

        public async Task DeleteAsync() =>
            await Client.Api.DeleteMessageAsync(ChannelId, Id);

        public async Task AcknowledgeAsync() =>
            await Client.Api.AcknowledgeMessageAsync(ChannelId, Id);

        public async Task RemoveReactionsAsync() =>
            await Client.Api.RemoveMessageReactionsAsync(ChannelId, Id);
    }
}
