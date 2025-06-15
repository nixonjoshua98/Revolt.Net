using Revolt.Net.Core.Entities;

namespace Revolt.Net.Rest.Contracts
{
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