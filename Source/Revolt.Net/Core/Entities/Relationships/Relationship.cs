using Revolt.Net.Core.Entities.Relationships;
using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public class Relationship
    {
        [JsonPropertyName("_id")]
        public string UserId { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}