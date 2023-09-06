namespace Revolt.Net.WebSocket
{
    internal sealed record ChannelCreatePayload(SocketChannel Channel)
    {
        public ChannelCreateEvent ToEvent() => new(Channel);
    }

    public sealed record ChannelCreateEvent(SocketChannel Channel);
}
