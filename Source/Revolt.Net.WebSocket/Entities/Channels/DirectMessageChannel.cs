namespace Revolt.Net.WebSocket
{
    public class DirectMessageChannel : MessageChannel
    {
        public bool Active { get; init; }

        public string[] Recipients { get; init; } = default!;
    }
}