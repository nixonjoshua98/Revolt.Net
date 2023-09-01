using Revolt.Net.Client;
using Revolt.Net.Commands._Original;
using Revolt.Net.Common.Json;
using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Websocket.Events.Incoming;
using Revolt.Net.Websocket.Events.Outgoing;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Revolt.Net.Websocket;

internal sealed class RevoltWebsocketConnection
{
    private readonly Uri Url;
    private readonly ClientWebSocket Ws;
    private readonly string Token;
    private readonly System.Timers.Timer PingTimer;
    private readonly RevoltClient Client;

    private CancellationToken ConnectionToken = default!;

    public TimeSpan SocketPing { get; private set; } = TimeSpan.Zero;

    public RevoltWebsocketConnection(RevoltClient client, string url, string token)
    {
        Client = client;
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
                Console.WriteLine($"Incoming : {JsonSerializer.Serialize(JsonNode.Parse(content), Serialization.Options)}");

                _ = OnMessageReceieved(content);
            }
        }
    }

    private Task OnMessageReceieved(string message)
    {
        var node = JsonNode.Parse(message);

        var type = node!["type"]!.ToString();

        var method = GetType()
            .GetMethod($"On{type}Event", BindingFlags.Instance | BindingFlags.NonPublic);

        if (method is not null)
        {
            var result = method.Invoke(this, new object[] { message });

            if (result is Task t)
            {
                return t;
            }
        }

        return Task.CompletedTask;
    }

    private Task OnChannelCreateEvent(string message)
    {
        var channel = Serialization.Deserialize<Channel>(message);

        return Client._ChannelCreated.InvokeAsync(
            new ChannelCreateInternalEvent(channel)
        );
    }

    private Task OnChannelDeleteEvent(string message)
    {
        return Client.ChannelDeleted.InvokeAsync(
            Serialization.Deserialize<ChannelDeleteEvent>(message)
        );
    }

    private Task OnServerDeleteEvent(string message)
    {
        return Client.ServerDeleted.InvokeAsync(
            Serialization.Deserialize<ServerDeleteEvent>(message)
        );
    }

    private Task OnUserRelationshipEvent(string message)
    {
        return Client._RelationshipUpdated.InvokeAsync(
            Serialization.Deserialize<UserRelationshipInternalEvent>(message)
        );
    }

    private Task OnUserUpdateEvent(string message)
    {
        return Client._UserUpdated.InvokeAsync(
            Serialization.Deserialize<UserUpdateInternalEvent>(message)
        );
    }

    private Task OnMessageDeleteEvent(string message)
    {
        return Client.MessageDeleted.InvokeAsync(
            Serialization.Deserialize<MessageDeleteEvent>(message)
        );
    }

    private Task OnMessageEvent(string message)
    {
        return Client._Message.InvokeAsync(
            new MessageEvent(Serialization.Deserialize<Message>(message)
        ));
    }

    private Task OnReadyEvent(string message)
    {
        PingTimer.Start();

        return Client._Ready.InvokeAsync(
            Serialization.Deserialize<ReadyInternalEvent>(message)
        );
    }

    private Task OnPongEvent(string message)
    {
        var e = Serialization.Deserialize<PongEvent>(message);

        SocketPing = TimeSpan.FromMilliseconds(
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - e.Data
        );

        return Task.CompletedTask;
    }

    private async Task AuthenticateAsync() =>
        await SendAsync(new AuthenticateEvent(Token));

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
        var json = JsonSerializer.Serialize(message, Serialization.Options);

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
