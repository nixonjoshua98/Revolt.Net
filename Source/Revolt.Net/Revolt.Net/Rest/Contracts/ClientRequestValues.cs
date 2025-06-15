using Revolt.Net.Core.Entities.Embeds;
using Revolt.Net.Core.JsonModels.Messages;

namespace Revolt.Net.Rest.Contracts
{
    internal sealed record SendMessageValues(
        string ChannelId,
        string? Content,
        IEnumerable<Embed> Embeds,
        IEnumerable<JsonMessageReply> Replies
    );

    internal sealed record PinMessageValues(
        string ChannelId,
        string MessageId
    );

    internal sealed record DeleteMessageValues(
        string ChannelId,
        string MessageId
    );

    internal sealed record GetMessageValues(
        string ChannelId,
        string MessageId
     );

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

    internal readonly record struct GetUserValues(
        string UserId
    );

    internal sealed record EditMessageValues(
        string ChannelId,
        string MessageId,
        string? Content,
        IEnumerable<Embed>? Embeds = null
    )
    {
        public object ToBody() => new { Content, Embeds };
    }
}