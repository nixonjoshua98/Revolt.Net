using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels
{
    internal sealed class JsonServer
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("owner")]
        public required string OwnerUserId { get; init; }

        public required string Name { get; init; }

        [JsonPropertyName("nsfw")]
        public bool IsNSFW { get; init; }
    }
}
