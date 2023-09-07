using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket
{
    internal sealed record ChannelCreatePayload(RestChannel Channel)
    {
        public ChannelCreateEvent ToEvent() => new(Channel);
    }

    public sealed record ChannelCreateEvent(RestChannel Channel);
}
