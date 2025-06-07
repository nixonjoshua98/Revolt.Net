using Revolt.Net.Core;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Events;
using Revolt.Net.WebSocket.Messages;

namespace Revolt.Net.WebSocket.Services
{
    internal sealed class WebSocketEventHub(RevoltRestClient _restClient) : IWebSocketEventHub
    {
        public AsyncEvent<ReadyWebSocketEvent> Ready { get; } = new();

        public AsyncEvent<SocketMessageReceivedEvent> Message { get; } = new();

        public async Task InvokeAsync<T>(T e, CancellationToken cancellationToken) where T : WebSocketEvent
        {
            switch (e)
            {
                case ReadyWebSocketEvent ready:
                    await Ready.InvokeAsync(ready, cancellationToken);
                    break;

                case MessageWebSocketEvent message:
                    var socketMessage = new Message(message.ToJsonMessage(), _restClient);
                    var payload = new SocketMessageReceivedEvent(socketMessage);
                    await Message.InvokeAsync(payload, cancellationToken);
                    break;

            }
        }
    }
}