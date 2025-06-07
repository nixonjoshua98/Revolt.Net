using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Messages
{
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ReadyWebSocketEvent), "Ready")]
    [JsonDerivedType(typeof(MessageWebSocketEvent), "Message")]
    public record WebSocketEvent;
}