﻿using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.JsonModels
{
    internal sealed record JsonChannelStopTyping : JsonWebSocketMessage
    {
        public required string Id { get; init; }

        [JsonPropertyName("user")]
        public required string UserId { get; init; }
    }
}
