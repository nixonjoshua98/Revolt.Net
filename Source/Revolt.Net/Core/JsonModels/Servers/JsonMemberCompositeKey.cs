using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels.Servers
{
    internal sealed class JsonMemberCompositeKey
    {
        [JsonPropertyName("server")]
        public required string ServerId { get; init; }

        [JsonPropertyName("user")]
        public required string UserId { get; init; }
    }
}
