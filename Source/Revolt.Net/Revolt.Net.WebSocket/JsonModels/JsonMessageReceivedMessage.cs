using Revolt.Net.Core.JsonModels;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.JsonModels
{
    internal sealed record JsonMessageReceivedMessage : JsonWebSocketMessage
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("author")]
        public required string AuthorId { get; init; }

        [JsonPropertyName("channel")]
        public required string ChannelId { get; init; }

        public string? Content { get; init; }

        public JsonServerMember? Member { get; init; }

        public JsonUser? User { get; init; }

        public JsonMessage ToJsonMessage() => new JsonMessage
        {
            Id = Id,
            AuthorId = AuthorId,
            ChannelId = ChannelId,
            Content = Content,
            Member = Member,
            User = User
        };
    }
}