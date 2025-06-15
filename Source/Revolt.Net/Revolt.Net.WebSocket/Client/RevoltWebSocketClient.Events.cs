using Revolt.Net.Core.Common;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Events;
using Revolt.Net.WebSocket.JsonModels;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket.Client
{
    internal sealed partial class RevoltWebSocketClient(RevoltRestClient _restClient, RevoltClientState _clientState) : IRevoltWebSocketClient
    {
        public AsyncEvent<ReadyEvent> Ready { get; } = new();

        public AsyncEvent<MessageReceivedEvent> MessageReceived { get; } = new();

        public AsyncEvent<ChannelStartTypingEvent> ChannelStartTyping { get; } = new();

        public AsyncEvent<ChannelStopTypingEvent> ChannelStopTyping { get; } = new();

        public AsyncEvent<JsonMessageReceivedMessage> RawMessageReceived { get; } = new();

        public async Task InvokeAsync<T>(T e, CancellationToken cancellationToken) where T : JsonWebSocketMessage
        {
            switch (e)
            {
                case JsonReadyMessage ready:
                    await Ready.InvokeAsync(ready.ToEvent(), cancellationToken);
                    break;

                case JsonMessageReceivedMessage message:
                    await RawMessageReceived.InvokeAsync(message, cancellationToken);
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

            }
        }
    }
}