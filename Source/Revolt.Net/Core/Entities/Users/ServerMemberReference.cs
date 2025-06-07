using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class ServerMemberReference
    {
        [JsonPropertyName("_id")]
        public required ServerMemberIdentifer Id { get; init; }
        public string? Nickname { get; init; }
        public Avatar? Avatar { get; init; }
        public required DateTimeOffset JoinedAt { get; init; }

        public string UserId => Id.User;
        public string ServerId => Id.Server;
    }

    public sealed record ServerMemberIdentifer(string Server, string User);
}
