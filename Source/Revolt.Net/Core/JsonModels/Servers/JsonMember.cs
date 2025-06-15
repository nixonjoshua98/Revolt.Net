using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels.Servers
{
    internal sealed class JsonMember
    {
        [JsonPropertyName("_id")]
        public required JsonMemberCompositeKey Id { get; init; }

        public required DateTimeOffset JoinedAt { get; init; }

        public string? Nickname { get; init; }

        public string[] Roles { get; init; } = [];
    }
}
