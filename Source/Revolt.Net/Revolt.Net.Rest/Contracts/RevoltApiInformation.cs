using System.Text.Json.Serialization;

namespace Revolt.Net.Rest.Contracts
{
    public sealed class RevoltApiInformation
    {
        [JsonPropertyName("ws")]
        public required string WebSocketUrl { get; init; }
    }
}
