using Revolt.Net.Core.Entities.Relationships;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestUser : IUser
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        public string Username { get; init; }

        public string Discriminator { get; init; }

        public string DisplayName { get; init; }

        public Avatar Avatar { get; init; }

        public IEnumerable<Relation> Relations { get; init; }

        public Badge Badges { get; init; }

        public UserStatus Status { get; init; }

        [JsonPropertyName("privileged")]
        public bool IsPrivileged { get; init; }

        public Optional<UserBot> Bot { get; init; }

        [JsonPropertyName("online")]
        public bool IsOnline { get; init; }

        public RelationshipStatus Relationship { get; init; }

        public bool IsBot => Bot.HasValue;

        internal void UpdateFromPartial(PartialUser user)
        {
            user.Status.Match(Status.UpdateFromPartial);
        }

    }
}
