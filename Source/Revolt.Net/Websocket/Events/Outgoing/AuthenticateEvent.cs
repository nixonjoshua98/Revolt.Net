namespace Revolt.Net.Websocket.Events.Outgoing;

internal sealed record AuthenticateEvent(string Token)
{
    public string Type { get; } = "Authenticate";
}
