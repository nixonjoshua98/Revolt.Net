using Revolt.Net.Core.Entities.Relationships;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestUser
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        public required string UserName { get; init; }

        public required string Discriminator { get; init; }

        public string? DisplayName { get; init; }

        public Avatar? Avatar { get; init; }

        public IReadOnlyList<Relation> Relations { get; init; } = [];

        public Badge Badges { get; init; }

        public UserStatus? Status { get; init; }

        [JsonPropertyName("privileged")]
        public bool IsPrivileged { get; init; }

        public UserBot? Bot { get; init; }

        [JsonPropertyName("online")]
        public bool IsOnline { get; init; }

        public required RelationshipStatus Relationship { get; init; }

        public bool IsBot => Bot is not null;

    }
}
