using Revolt.Net.Core.Entities;
using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Contracts
{
    internal sealed record SendMessageValues(
        string ChannelId,
        string? Content,
        IEnumerable<Embed> Embeds,
        IEnumerable<JsonMessageReply> Replies
    );
}