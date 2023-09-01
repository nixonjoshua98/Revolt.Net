using Revolt.Net.Entities.Users;
using System.Text.Json.Serialization;

namespace Revolt.Net.Entities.Relationships
{
    public class Relationship
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public string UserId => Id;

        public RelationshipStatus Status { get; init; }
    }
}