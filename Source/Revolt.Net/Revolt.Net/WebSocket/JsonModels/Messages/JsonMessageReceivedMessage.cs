using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Core.JsonModels.Messages;
using Revolt.Net.Core.JsonModels.Servers;
using Revolt.Net.Core.JsonModels.Users;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Events;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.JsonModels.Messages
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

        public MessageReceivedEvent ToEvent(RevoltRestClient restClient)
        {
            var message = new JsonMessage
            {
                Id = Id,
                AuthorId = AuthorId,
                ChannelId = ChannelId,
                Content = Content,
                Member = Member,
                User = User
            };

            var socketMessage = Message.CreateNew(message, restClient);

            return new MessageReceivedEvent(socketMessage);
        }
    }
}