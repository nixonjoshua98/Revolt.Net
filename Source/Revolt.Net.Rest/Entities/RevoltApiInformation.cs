using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class RevoltApiInformation
    {
        [JsonPropertyName("ws")]
        public string WebsocketUrl { get; init; } = default!;
    }
}
