using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class User : IUser
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }
        public string Username { get; init; }
        public Relation[] Relations { get; init; }
        public UserStatus Status { get; init; }
        public Badge Badges { get; init; }
        public RelationshipStatus Relationship { get; init; }
        public bool Online { get; init; }
        public Optional<UserBot> Bot { get; init; }

        public bool IsBot => Bot.HasValue;

        internal void UpdateFromPartial(PartialUser user)
        {
            user.Status.Match(Status.UpdateFromPartial);
        }
    }
}
