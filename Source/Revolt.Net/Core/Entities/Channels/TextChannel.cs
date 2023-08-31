using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Channels
{
    public sealed class TextChannel : Channel
    {
        [JsonPropertyName("server")]
        public string ServerId { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public string LastMessageId { get; init; } = default!;

        public bool Nsfw { get; init; }
    }
}
