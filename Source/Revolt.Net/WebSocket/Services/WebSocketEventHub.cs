using Revolt.Net.Core;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Messages;

namespace Revolt.Net.WebSocket.Services
{
    internal sealed class WebSocketEventHub : IWebSocketEventHub
    {
        public AsyncEvent<ReadyWebSocketEvent> Ready { get; } = new();
        public AsyncEvent<MessageWebSocketEvent> Message { get; } = new();

        public async Task InvokeAsync<T>(T message, CancellationToken cancellationToken) where T : WebSocketEvent
        {
            await (message switch
            {
                ReadyWebSocketEvent ready => Ready.InvokeAsync(ready, cancellationToken),
                MessageWebSocketEvent msg => Message.InvokeAsync(msg, cancellationToken),
                _ => Task.CompletedTask
            });
        }
    }
}