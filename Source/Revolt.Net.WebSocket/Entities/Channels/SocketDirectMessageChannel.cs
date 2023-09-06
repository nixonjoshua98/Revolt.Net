namespace Revolt.Net.WebSocket
{
    public class SocketDirectMessageChannel : SocketMessageChannel
    {
        public bool Active { get; init; }

        public string[] Recipients { get; init; } = default!;
    }
}