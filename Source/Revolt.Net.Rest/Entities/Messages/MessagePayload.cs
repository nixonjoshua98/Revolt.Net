using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    internal sealed class MessagePayload
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; }

        [JsonPropertyName("author")]
        public string AuthorId { get; init; }

        public Optional<string> Content { get; init; }
    }
}
