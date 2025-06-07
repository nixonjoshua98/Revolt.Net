using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels.Messages
{
    internal sealed record JsonMessage
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("author")]
        public required string AuthorId { get; init; }

        [JsonPropertyName("channel")]
        public required string ChannelId { get; init; }

        public string? Content { get; init; }
    }
}
