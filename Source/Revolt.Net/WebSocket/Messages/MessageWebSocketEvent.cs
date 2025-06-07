using Revolt.Net.Core.JsonModels.Messages;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Messages
{
    internal sealed record MessageWebSocketEvent : WebSocketEvent
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("author")]
        public required string AuthorId { get; init; }

        [JsonPropertyName("channel")]
        public required string ChannelId { get; init; }

        public string? Content { get; init; }

        internal JsonMessage ToJsonMessage()
        {
            return new JsonMessage
            {
                Id = Id,
                AuthorId = AuthorId,
                ChannelId = ChannelId,
                Content = Content,
            };
        }
    }
}