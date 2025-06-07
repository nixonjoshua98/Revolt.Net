using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestUser
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        [JsonPropertyName("username")]
        public required string UserName { get; init; }

        public required string Discriminator { get; init; }

        public string? DisplayName { get; init; }

        [JsonPropertyName("privileged")]
        public bool IsPrivileged { get; init; }

        public UserBot? Bot { get; init; }

        [JsonPropertyName("online")]
        public bool IsOnline { get; init; }

        public bool IsBot => Bot is not null;

    }
}
