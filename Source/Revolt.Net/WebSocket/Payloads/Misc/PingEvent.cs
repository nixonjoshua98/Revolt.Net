namespace Revolt.Net.WebSocket
{
    internal sealed record PingEvent
    {
        public string Type { get; } = "Ping";
        public long Data { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}