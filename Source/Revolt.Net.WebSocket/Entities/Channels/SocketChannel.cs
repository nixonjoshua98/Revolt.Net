using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public abstract class SocketChannel : SocketEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public ChannelType ChannelType { get; init; }
    }
}
