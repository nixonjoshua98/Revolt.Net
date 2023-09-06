﻿namespace Revolt.Net.WebSocket
{
    public sealed record ServerMember(
        string UserId,
        string ServerId,
        string Nickname,
        Attachment Avatar,
        DateTimeOffset JoinedAt
    )
    {
        internal static ServerMember Create(ServerMemberReference reference, SocketUser user)
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
