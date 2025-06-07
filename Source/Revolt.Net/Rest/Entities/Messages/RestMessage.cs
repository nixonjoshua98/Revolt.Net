using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public class RestMessage : RestEntity
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
    }
}
