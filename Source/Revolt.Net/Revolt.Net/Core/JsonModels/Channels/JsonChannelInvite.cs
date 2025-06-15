using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels.Channels
{
    internal sealed class JsonChannelInvite
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("server")]
        public required string ServerId { get; init; }

        [JsonPropertyName("channel")]
        public required string ChannelId { get; init; }

        [JsonPropertyName("creator")]
        public required string CreatorUserId { get; init; }
    }
}
