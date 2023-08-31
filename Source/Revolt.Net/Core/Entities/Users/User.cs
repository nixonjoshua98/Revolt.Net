using Revolt.Net.Core.Entities.Relationships;
using Revolt.Net.Core.Entities.Users.Partials;
using Revolt.Net.Core.Enums;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Users
{
    public class User
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        public string Username { get; init; } = default!;
        public Relation[] Relations { get; init; } = default!;
        public UserStatus Status { get; init; } = default!;
        public Badge Badges { get; init; } = default!;
        public RelationshipStatus Relationship { get; init; } = default!;
        public bool Online { get; init; } = default!;

        internal void UpdateFromPartial(PartialUser user)
        {
            user.Status.WhenHasValue(Status.UpdateFromPartial);
        }
    }
}
