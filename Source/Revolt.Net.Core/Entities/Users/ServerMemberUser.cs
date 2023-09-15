namespace Revolt.Net
{
    public sealed record ServerMemberUser(
        string Id,
        string ServerId,
        string Nickname,
        Avatar Avatar,
        DateTimeOffset JoinedAt
    )
    {
        internal static ServerMemberUser Create(ServerMember reference, IUser user)
        {
            return new(
                user.Id,
                reference.ServerId,
                reference.Nickname,
                reference.Avatar,
                reference.JoinedAt
            );
        }
    }
}
