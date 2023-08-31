using Revolt.Net.Core.Enums;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Channels
{
    public class Channel
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public ChannelType ChannelType { get; init; }
    }
}
