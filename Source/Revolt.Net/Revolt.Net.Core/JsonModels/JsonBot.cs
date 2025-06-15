using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels
{
    internal sealed class JsonBot
    {
        [JsonPropertyName("owner")]
        public required string OwnerId { get; init; }
    }
}
