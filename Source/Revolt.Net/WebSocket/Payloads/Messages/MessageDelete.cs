namespace Revolt.Net.WebSocket
{
    internal sealed record MessageDeletePayload(string Id, string Channel)
    {
        public MessageDeleteEvent ToEvent() => new(Channel, Id);
    }

    public sealed record MessageDeleteEvent(string ChannelId, string MessageId);
}
