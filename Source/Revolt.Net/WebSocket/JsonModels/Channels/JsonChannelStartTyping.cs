using Revolt.Net.WebSocket.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.JsonModels.Channels
{
    internal sealed record JsonChannelStartTyping : JsonWebSocketMessage
    {
        public required string Id { get; init; }

        [JsonPropertyName("user")]
        public required string UserId { get; init; }
    }
}
