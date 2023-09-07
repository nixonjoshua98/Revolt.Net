namespace Revolt.Net.WebSocket
{

    internal sealed class ServerDeletePayload
    {
        public string Id { get; init; } = default!;

        public ServerDeleteEvent ToEvent() => new(Id);
    }

    public sealed record ServerDeleteEvent(string ServerId);
}