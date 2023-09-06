using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.Payloads
{
    internal sealed class MessageUpdate
    {
        [JsonPropertyName("id")]
        public string MessageId { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; }

        public Message Data { get; init; }
    }
}
