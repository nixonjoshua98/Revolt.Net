using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Messages
{
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ReadyWebSocketEvent), "Ready")]
    public record WebSocketEvent;
}