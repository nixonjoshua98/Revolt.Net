using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public abstract class Channel : RestEntity, IChannel
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        public ChannelType ChannelType { get; init; }
    }
}
