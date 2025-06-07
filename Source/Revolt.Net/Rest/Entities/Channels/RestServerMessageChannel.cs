using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestServerMessageChannel : RestTextChannel
    {
        [JsonPropertyName("server")]
        public string ServerId { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public string LastMessageId { get; init; } = default!;

        public bool Nsfw { get; init; }
    }
}
