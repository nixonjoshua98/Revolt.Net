using Revolt.Net.WebSocket.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Revolt.Net.WebSocket;

internal sealed class RevoltWebSocketConnection
{
    private readonly Uri Url;
    private readonly ClientWebSocket Ws;
    private readonly string Token;
    private readonly System.Timers.Timer PingTimer;

    private CancellationToken ConnectionToken = default!;

    public TimeSpan SocketPing { get; private set; } = TimeSpan.Zero;

    internal event Func<WebSocketMessage, Task> MessageReceived = default!;

    public RevoltWebSocketConnection(string url, string token)
    {
        Url = new(url);
        Ws = new();
        Token = token;

        PingTimer = new System.Timers.Timer(TimeSpan.FromSeconds(15));

        PingTimer.Elapsed += (sender, args) => _ = PingAsync();
    }

    public async Task RunAsync(CancellationToken ct)
    {
        ConnectionToken = ct;

        var buffer = new byte[4096];

        await ConnectAsync();

        while (true)
        {
            var (type, content) = await ReceiveAsync(buffer);

            if (type == WebSocketMessageType.Text)
            {
                Console.WriteLine($"Incoming : {JsonSerializer.Serialize(JsonNode.Parse(content), WebSocketSerialization.Options)}");

                _ = OnMessageReceieved(content);
            }
        }
    }

    private Task OnMessageReceieved(string message)
    {
        var node = JsonNode.Parse(message);

        var type = node!["type"]!.ToString();

        return type switch
        {
            "Pong" => OnPongEvent(node),
            _ => MessageReceived?.Invoke(new WebSocketMessage(type, node))
        } ?? Task.CompletedTask;
    }

    private Task OnPongEvent(JsonNode message)
    {
        var e = message.Deserialize<PongEvent>(WebSocketSerialization.Options);

        SocketPing = TimeSpan.FromMilliseconds(
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - e.Data
        );

        return Task.CompletedTask;
    }

    private async Task AuthenticateAsync() =>
        await SendAsync(new AuthenticatePayload(Token));

    private async Task PingAsync() =>
        await SendAsync(new PingEvent());

    private async Task ConnectAsync()
    {
        await Ws.ConnectAsync(Url, ConnectionToken);

        await AuthenticateAsync();
    }

    public async Task DisconnectAsync(CancellationToken ct = default)
    {
        await Ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, ct);
    }

    internal async Task SendAsync<T>(T message) where T : class
    {
        var json = JsonSerializer.Serialize(message, WebSocketSerialization.Options);

        var bytes = Encoding.UTF8.GetBytes(json);

        await Ws.SendAsync(bytes, WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, ConnectionToken);
    }

    private async Task<(WebSocketMessageType Type, string Content)> ReceiveAsync(byte[] buffer)
    {
        while (true)
        {
            var result = await Ws.ReceiveAsync(buffer, ConnectionToken);

            var content = Encoding.UTF8.GetString(buffer, 0, result.Count);

            return (result.MessageType, content);
        }
    }
}
