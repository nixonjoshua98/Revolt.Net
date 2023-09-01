using Revolt.Net.Entities.Media;

namespace Revolt.Net.Entities.Users
{
    public sealed record ServerMember(
        string UserId,
        string ServerId,
        string Nickname,
        Attachment Avatar,
        DateTimeOffset JoinedAt
    )
    {
        internal static ServerMember Create(ServerMemberReference reference, User user)
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
