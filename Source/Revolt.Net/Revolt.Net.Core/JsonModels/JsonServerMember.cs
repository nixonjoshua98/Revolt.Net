using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels
{
    internal sealed class JsonServerMember
    {
        [JsonPropertyName("_id")]
        public required JsonMemberCompositeKey Id { get; init; }

        public required DateTimeOffset JoinedAt { get; init; }

        public string? Nickname { get; init; }

        [JsonPropertyName("roles")]
        public string[] RoleIds { get; init; } = [];
    }
}
