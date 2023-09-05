using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public sealed class ServerMemberReference
    {
        [JsonPropertyName("_id")]
        public ServerMemberIdentifer Id { get; init; } = default!;
        public string UserId => Id.User;
        public string ServerId => Id.Server;
        public string Nickname { get; init; }
        public Attachment Avatar { get; init; } = default!;
        public DateTimeOffset JoinedAt { get; init; } = default!;
    }

    public sealed record ServerMemberIdentifer(string Server, string User);
}
