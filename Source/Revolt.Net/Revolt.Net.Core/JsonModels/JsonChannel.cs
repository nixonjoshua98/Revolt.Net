using Revolt.Net.Core.Enumerations;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels
{
    public class JsonChannel
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        public required ChannelType ChannelType { get; init; }
    }
}
