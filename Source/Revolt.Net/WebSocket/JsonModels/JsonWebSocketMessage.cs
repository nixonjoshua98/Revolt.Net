using Revolt.Net.WebSocket.JsonModels;
using Revolt.Net.WebSocket.JsonModels.Channels;
using Revolt.Net.WebSocket.JsonModels.Messages;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Messages
{
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(JsonReadyMessage), "Ready")]
    [JsonDerivedType(typeof(JsonMessageReceivedMessage), "Message")]
    [JsonDerivedType(typeof(JsonChannelStartTyping), "ChannelStartTyping")]
    [JsonDerivedType(typeof(JsonChannelStopTyping), "ChannelStopTyping")]
    [JsonDerivedType(typeof(JsonBulk), "Bulk")]
    public record JsonWebSocketMessage;
}