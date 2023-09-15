using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class ServerMember
    {
        [JsonPropertyName("_id")]
        public ServerMemberIdentifer Identifier { get; init; }

        public string Id => Identifier.User;
        public string ServerId => Identifier.Server;
        public string Nickname { get; init; }
        public Avatar Avatar { get; init; }
        public DateTimeOffset JoinedAt { get; init; }
    }

    public sealed record ServerMemberIdentifer(string Server, string User);
}
