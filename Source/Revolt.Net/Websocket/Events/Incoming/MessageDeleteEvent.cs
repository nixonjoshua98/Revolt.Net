using System.Text.Json.Serialization;

namespace Revolt.Net.Websocket.Events.Incoming
{
    public sealed class MessageDeleteEvent
    {
        [JsonPropertyName("id")]
        public string MessageId { get; init; } = default!;

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; } = default!;
    }
}
