namespace Revolt.Net.WebSocket
{
    internal sealed record ChannelCreatePayload(Channel Channel)
    {
        public ChannelCreateEvent ToEvent() => new(Channel);
    }

    public sealed record ChannelCreateEvent(Channel Channel);
}
