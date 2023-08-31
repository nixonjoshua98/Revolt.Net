using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Common;
using Revolt.Net.Core.Entities.Users;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Messages
{
    public sealed class Message : RevoltEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public string? Nonce { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; } = default!;

        [JsonPropertyName("author")]
        public string AuthorId { get; init; } = default!;

        public string Content { get; init; } = default!;

        [JsonIgnore]
        public Channel? Channel => Client.GetChannel(ChannelId);

        [JsonIgnore]
        public User? Author => Client.GetUser(AuthorId);
    }
}
