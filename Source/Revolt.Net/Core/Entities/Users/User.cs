using Revolt.Net.Core.Entities.Relationships;
using Revolt.Net.Core.Entities.Users.Partials;
using Revolt.Net.Core.Enums;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Users
{
    public class User
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }
        public string Username { get; init; }
        public Relation[] Relations { get; init; }
        public UserStatus Status { get; init; }
        public Badge Badges { get; init; }
        public RelationshipStatus Relationship { get; init; }
        public bool Online { get; init; }
        public UserBot Bot { get; init; }

        internal void UpdateFromPartial(PartialUser user)
        {
            user.Status.WhenHasValue(Status.UpdateFromPartial);
        }
    }
}
