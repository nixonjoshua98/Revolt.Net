namespace Revolt.Net.Rest.Contracts.RestValues
{
    internal sealed record EditMessageValues(
        string ChannelId,
        string MessageId,
        string? Content,
        IEnumerable<Embed>? Embeds = null
    )
    {
        public object ToBody()
        {
            return new { Content, Embeds };
        }
    }
}