using Microsoft.Extensions.Logging;
using Revolt.Net.Core;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Events.Channels;
using Revolt.Net.WebSocket.Events.Messages;
using Revolt.Net.WebSocket.JsonModels;
using Revolt.Net.WebSocket.JsonModels.Channels;
using Revolt.Net.WebSocket.JsonModels.Messages;
using Revolt.Net.WebSocket.Messages;

namespace Revolt.Net.WebSocket.Services
{
    internal sealed class RevoltWebSocketClient(
        RevoltRestClient restClient,
        ILogger<RevoltWebSocketClient> _logger
    ) : IRevoltWebSocketClient
    {
        public AsyncEvent<JsonReadyMessage> Ready { get; } = new();

        public AsyncEvent<MessageEvent> Message { get; } = new();

        public AsyncEvent<ChannelStartTypingEvent> ChannelStartTyping { get; } = new();

        public AsyncEvent<ChannelStopTypingEvent> ChannelStopTyping { get; } = new();

        public RevoltRestClient RestClient { get; } = restClient;

        public async Task InvokeAsync<T>(T e, CancellationToken cancellationToken) where T : JsonWebSocketMessage
        {
            switch (e)
            {
                case JsonReadyMessage ready:
                    await Ready.InvokeAsync(ready, cancellationToken);
                    break;

                case JsonMessageReceivedMessage message:
                    var socketMessage = new Message(message.ToJsonMessage(), RestClient);
                    var payload = new MessageEvent(socketMessage);
                    await Message.InvokeAsync(payload, cancellationToken);
                    break;

                case JsonChannelStartTyping start:
                    await ChannelStartTyping.InvokeAsync(new(start), cancellationToken);
                    break;

                case JsonChannelStopTyping stop:
                    await ChannelStopTyping.InvokeAsync(new(stop), cancellationToken);
                    break;

                case JsonBulk bulk:
                    await Task.WhenAll(bulk.Messages.Select(x => InvokeAsync(x, cancellationToken)));
                    break;

                default:
                    _logger.LogDebug("Unsupported message : {MessageType}", e.GetType().Name);
                    break;

            }
        }
    }
}