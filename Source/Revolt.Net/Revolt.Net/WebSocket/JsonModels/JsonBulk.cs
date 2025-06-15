using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.JsonModels
{
    internal sealed record JsonBulk : JsonWebSocketMessage
    {
        [JsonPropertyName("v")]
        public required JsonWebSocketMessage[] Messages { get; init; }
    }
}
