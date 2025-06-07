using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Members
{
    public sealed class ServerMemberReference
    {
        [JsonPropertyName("_id")]
        public required ServerMemberIdentifer Id { get; init; }
        public string? Nickname { get; init; }
        public required DateTimeOffset JoinedAt { get; init; }

        public string UserId => Id.User;
        public string ServerId => Id.Server;
    }

    public readonly record struct ServerMemberIdentifer(string Server, string User);
}
