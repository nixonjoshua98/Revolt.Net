using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Entities.Servers
{
    public sealed class SocketServer
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("owner")]
        public required string OwnerId { get; init; }

        [JsonPropertyName("channels")]
        public IReadOnlyList<string> ChannelIds { get; init; } = [];
    }
}
