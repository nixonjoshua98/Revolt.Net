using System.Text.Json.Serialization;

namespace Revolt.Net
{
    /// <summary>
    /// Message entity which contains all possible properties. Useful for loading data which may not be present and can be re-used for
    /// sockets, api responses, and partial messages.
    /// </summary>
    internal sealed class Message
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
