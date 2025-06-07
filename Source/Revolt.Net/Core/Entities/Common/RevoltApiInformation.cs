using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class RevoltApiInformation
    {
        [JsonPropertyName("ws")]
        public required string WebSocketUrl { get; init; }
    }
}
