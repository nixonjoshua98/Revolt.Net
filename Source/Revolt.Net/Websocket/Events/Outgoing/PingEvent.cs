namespace Revolt.Net.Websocket.Events.Outgoing;

internal sealed record PingEvent
{
    public string Type { get; } = "Ping";
    public long Data { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}
