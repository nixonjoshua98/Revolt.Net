using Revolt.Net.WebSocket.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.JsonModels
{
    internal sealed record JsonBulk : JsonWebSocketMessage
    {
        [JsonPropertyName("v")]
        public required JsonWebSocketMessage[] Messages { get; init; }
    }
}
