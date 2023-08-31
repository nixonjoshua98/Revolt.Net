using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Messages
{
    public sealed class Message
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public string? Nonce { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; } = default!;

        [JsonPropertyName("author")]
        public string AuthorId { get; init; } = default!;

        public string Content { get; init; } = default!;
    }
}
