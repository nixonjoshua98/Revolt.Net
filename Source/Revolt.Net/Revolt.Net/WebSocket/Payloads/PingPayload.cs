namespace Revolt.Net.WebSocket.Payloads
{
    internal sealed record PingPayload
    {
        public string Type { get; init; } = "Ping";
    }
}