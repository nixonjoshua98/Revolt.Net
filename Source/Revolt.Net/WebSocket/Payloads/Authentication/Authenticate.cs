namespace Revolt.Net.WebSocket
{
    internal sealed record AuthenticatePayload(string Token)
    {
        public string Type { get; } = "Authenticate";
    }
}