using Revolt.Net.Core.JsonModels.Messages;

namespace Revolt.Net.Rest.Contracts.RestValues
{
    internal sealed record SendMessageValues(
        string ChannelId,
        string? Content,
        IEnumerable<Embed> Embeds,
        IEnumerable<JsonMessageReply> Replies
    );
}