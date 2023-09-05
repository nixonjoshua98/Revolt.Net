using Revolt.Net.Core.Entities.Users;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class Relationship
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public string UserId => Id;

        public RelationshipStatus Status { get; init; }
    }
}