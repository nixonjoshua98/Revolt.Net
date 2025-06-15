using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels
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

        public JsonUser? User { get; init; }

        public JsonServerMember? Member { get; init; }
    }
}
