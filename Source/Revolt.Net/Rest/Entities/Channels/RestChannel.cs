using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public class RestChannel
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        public required ChannelType ChannelType { get; init; }
    }
}
