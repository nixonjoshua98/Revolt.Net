namespace Revolt.Net.WebSocket
{
    internal sealed class ChannelDeletePayload
    {
        public string Id { get; init; } = default!;

        public ChannelDeleteEvent ToEvent() => new(Id);
    }

    public sealed record ChannelDeleteEvent(string ChannelId);
}
