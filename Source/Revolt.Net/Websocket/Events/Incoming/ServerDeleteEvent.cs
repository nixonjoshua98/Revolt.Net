using System.Text.Json.Serialization;

namespace Revolt.Net.Websocket.Events.Incoming;

public sealed class ServerDeleteEvent
{
    [JsonPropertyName("id")]
    public string ServerId { get; init; } = default!;
}
