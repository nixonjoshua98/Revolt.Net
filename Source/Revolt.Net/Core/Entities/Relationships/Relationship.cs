using Revolt.Net.Core.Enums;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Relationships
{
    public class Relationship
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public string UserId => Id;

        public RelationshipStatus Status { get; init; }
    }
}