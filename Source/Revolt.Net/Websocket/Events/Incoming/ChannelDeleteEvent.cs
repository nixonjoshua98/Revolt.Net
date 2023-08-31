using System.Text.Json.Serialization;

namespace Revolt.Net.Websocket.Events.Incoming
{
    public sealed class ChannelDeleteEvent
    {
        [JsonPropertyName("id")]
        public string ChannelId { get; init; } = default!;
    }
}
