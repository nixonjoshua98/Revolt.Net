using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public class RestMessage
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("channel")]
        public required string ChannelId { get; init; }

        [JsonPropertyName("author")]
        public required string AuthorId { get; init; }

        public string? Content { get; init; }
    }
}
