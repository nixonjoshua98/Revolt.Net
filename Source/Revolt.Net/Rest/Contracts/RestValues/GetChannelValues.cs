namespace Revolt.Net.Rest.Contracts.RestValues
{
    internal readonly record struct GetChannelValues(
       string ChannelId
    );

    internal readonly record struct CreateChannelInviteValues(
        string ChannelId
    );

    internal readonly record struct GetServerMemberValues(
        string ServerId,
        string UserId
    );
}