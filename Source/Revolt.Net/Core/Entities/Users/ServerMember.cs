namespace Revolt.Net
{
    public sealed record ServerMember(
        string UserId,
        string ServerId,
        string Nickname,
        DateTimeOffset JoinedAt
    );
}
