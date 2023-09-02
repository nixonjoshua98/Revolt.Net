using Revolt.Net.Entities.Relationships;
using Revolt.Net.Entities.Users.Partial;
using System.Text.Json.Serialization;

namespace Revolt.Net.Entities.Users
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

        public bool IsBot => Bot is not null;

        internal void UpdateFromPartial(PartialUser user)
        {
            user.Status.WhenHasValue(Status.UpdateFromPartial);
        }
    }
}
