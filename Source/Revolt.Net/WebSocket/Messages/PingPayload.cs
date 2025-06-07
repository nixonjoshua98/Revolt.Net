namespace Revolt.Net.WebSocket.Messages
{
    internal sealed record PingPayload
    {
        public string Type { get; init; } = "Ping";
    }
}