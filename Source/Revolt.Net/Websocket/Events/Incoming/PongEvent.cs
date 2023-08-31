namespace Revolt.Net.Websocket.Events.Incoming;

internal sealed record PongEvent
{
    public long Data { get; init; }
}
