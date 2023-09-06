using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public sealed class SocketTextChannel : MessageChannel
    {
        [JsonPropertyName("server")]
        public string ServerId { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public string LastMessageId { get; init; } = default!;

        public bool Nsfw { get; init; }
    }
}
