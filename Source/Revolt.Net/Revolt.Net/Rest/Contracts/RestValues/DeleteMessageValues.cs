namespace Revolt.Net.Rest.Contracts.RestValues
{
    internal sealed record DeleteMessageValues(
        string ChannelId,
        string MessageId
    );
}