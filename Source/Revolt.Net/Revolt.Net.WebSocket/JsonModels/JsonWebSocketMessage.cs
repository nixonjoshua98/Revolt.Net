using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.JsonModels
{
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(JsonReadyMessage), "Ready")]
    [JsonDerivedType(typeof(JsonMessageReceivedMessage), "Message")]
    [JsonDerivedType(typeof(JsonChannelStartTyping), "ChannelStartTyping")]
    [JsonDerivedType(typeof(JsonChannelStopTyping), "ChannelStopTyping")]
    [JsonDerivedType(typeof(JsonBulk), "Bulk")]
    public record JsonWebSocketMessage;
}