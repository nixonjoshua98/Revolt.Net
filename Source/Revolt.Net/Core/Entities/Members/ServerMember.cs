namespace Revolt.Net.Core.Entities.Members
{
    public sealed record ServerMember(
        string UserId,
        string ServerId,
        string Nickname,
        DateTimeOffset JoinedAt
    );
}
