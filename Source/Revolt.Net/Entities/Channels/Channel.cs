using Revolt.Net.Entities.Common;
using System.Text.Json.Serialization;

namespace Revolt.Net.Entities.Channels
{
    public class Channel : RevoltEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public ChannelType ChannelType { get; init; }
    }
}
