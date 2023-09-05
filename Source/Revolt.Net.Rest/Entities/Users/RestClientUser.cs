using Revolt.Net.Core;
using Revolt.Net.Core.Entities.Users;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest.Entities.Users
{
    public sealed class RestClientUser : IUser
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }

        public string Username { get; init; }

        public string Discriminator { get; init; }

        public Optional<UserBot> Bot { get; init; }
    }
}
