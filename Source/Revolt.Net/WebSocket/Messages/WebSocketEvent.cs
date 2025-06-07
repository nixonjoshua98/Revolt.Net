using Revolt.Net.Core.Abstractions;
using Revolt.Net.WebSocket.Models.Messages;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Messages
{
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ReadyWebSocketEvent), "Ready")]
    [JsonDerivedType(typeof(MessageWebSocketEvent), "Message")]
    public record WebSocketEvent;

    public sealed record MessageWebSocketEvent : WebSocketEvent, IMessage
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("author")]
        public required string AuthorId { get; init; }

        [JsonPropertyName("channel")]
        public required string ChannelId { get; init; }

        public string? Content { get; init; }

        public SocketMessage ToMessage()
        {
            return new SocketMessage
            {
                Id = Id,
                AuthorId = AuthorId,
                ChannelId = ChannelId,
                Content = Content,
            };
        }
    }
}