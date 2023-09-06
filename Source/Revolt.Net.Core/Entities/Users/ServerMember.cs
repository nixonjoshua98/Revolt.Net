namespace Revolt.Net
{
    public sealed record ServerMember(
        string UserId,
        string ServerId,
        string Nickname,
        Avatar Avatar,
        DateTimeOffset JoinedAt
    )
    {
        internal static ServerMember Create(ServerMemberReference reference, IUser user)
        {
            return new(
                user.Id,
                reference.Id.Server,
                reference.Nickname,
                reference.Avatar,
                reference.JoinedAt
            );
        }
    }
}
