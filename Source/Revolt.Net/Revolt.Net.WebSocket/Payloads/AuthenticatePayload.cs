namespace Revolt.Net.WebSocket.Payloads
{
    internal sealed record AuthenticatePayload(string Token)
    {
        public string Type { get; init; } = "Authenticate";
    }
}