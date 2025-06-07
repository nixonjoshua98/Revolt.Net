using Revolt.Net.Rest;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Payloads
{
    internal sealed class MessageUpdate
    {
        [JsonPropertyName("id")]
        public string MessageId { get; init; }

        [JsonPropertyName("channel")]
        public string ChannelId { get; init; }

        public MessagePayload Data { get; init; }
    }
}
