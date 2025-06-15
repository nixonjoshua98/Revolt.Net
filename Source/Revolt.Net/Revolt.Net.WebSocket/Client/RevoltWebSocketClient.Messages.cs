using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Contracts;
using Revolt.Net.WebSocket.Entities;

namespace Revolt.Net.WebSocket.Client
{
    internal sealed partial class RevoltWebSocketClient
    {
        public async Task PinMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default) =>
            await _restClient.PinMessageAsync(channelId, messageId, cancellationToken);

        public async Task UnPinMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default) =>
            await _restClient.UnPinMessageAsync(channelId, messageId, cancellationToken);

        public async Task<Message> SendMessageAsync(SendMessageValues values, CancellationToken cancellationToken = default)
        {
            var message = await _restClient.SendMessageAsync(values, cancellationToken);

            return await CreateMessageAsync(message.JsonModel, cancellationToken);
        }

        public async Task DeleteMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default) =>
            await _restClient.DeleteMessageAsync(channelId, messageId, cancellationToken);

        public async Task<Message> GetMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default)
        {
            var message = await _restClient.GetMessageAsync(channelId, messageId, cancellationToken);

            return await CreateMessageAsync(message.JsonModel, cancellationToken);
        }

        public async Task<Message> EditMessageAsync(EditMessageValues values, CancellationToken cancellationToken = default)
        {
            var message = await _restClient.EditMessageAsync(values, cancellationToken);

            return await CreateMessageAsync(message.JsonModel, cancellationToken);
        }

        public async Task<Message> CreateMessageAsync(JsonMessage message, CancellationToken cancellationToken)
        {
            if (message.Member is not null)
            {
                var server = await GetServerAsync(message.Member.Id.ServerId, cancellationToken);

                return new ServerMessage(message, server, this);
            }

            return new Message(message, this);
        }
    }
}
