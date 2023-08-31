using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Channels
{
    public class GroupChannel : MessageChannel
    {
        public string Name { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        public string Description { get; init; } = default!;
    }
}