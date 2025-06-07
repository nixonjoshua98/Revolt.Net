namespace Revolt.Net.WebSocket.Models
{
    internal sealed record AuthenticatePayload(string Token)
    {
        public string Type { get; init; } = "Authenticate";
    }
}