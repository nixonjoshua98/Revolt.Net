using Microsoft.Extensions.Hosting;
using Revolt.Net.WebSocket.Client;
using Revolt.Net.WebSocket.Events;
using Revolt.Net.WebSocket.JsonModels;

namespace Revolt.Net.WebSocket.BackgroundServices
{
    internal sealed class WebSocketEventHandler(
        IRevoltWebSocketClient _socketClient
    ) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _socketClient.RawMessageReceived.Add(OnRawMessageReceivedAsync);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _socketClient.RawMessageReceived.Remove(OnRawMessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task OnRawMessageReceivedAsync(JsonMessageReceivedMessage data, CancellationToken cancellationToken)
        {
            var jsonMessage = data.ToJsonMessage();

            var message = await _socketClient.CreateMessageAsync(jsonMessage, cancellationToken);

            await _socketClient.MessageReceived.InvokeAsync(
                new MessageReceivedEvent(message),
                cancellationToken
            );
        }
    }
}
