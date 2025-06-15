namespace Revolt.Net.Rest.Contracts.RestValues
{
    internal sealed record GetMessageValues(
        string ChannelId,
        string MessageId
     );
}