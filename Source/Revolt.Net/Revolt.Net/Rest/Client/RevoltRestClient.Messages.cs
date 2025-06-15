using Revolt.Net.Core.Common;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Core.JsonModels.Messages;
using Revolt.Net.Rest.Contracts.RestValues;
using System.Net.Http.Json;

namespace Revolt.Net.Rest.Clients
{
    internal partial class RevoltRestClient
    {
        internal async Task<Message> SendMessageAsync(SendMessageValues values, CancellationToken cancellationToken = default)
        {
            using var content = JsonContent.Create(values, options: Serialization.DefaultOptions);

            var response = await SendRequestAsync<JsonMessage>(HttpMethod.Post, content, $"channels/{values.ChannelId}/messages", cancellationToken);

            return Message.CreateNew(response, this);
        }

        internal async Task DeleteMessageAsync(DeleteMessageValues values, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync(HttpMethod.Delete, $"channels/{values.ChannelId}/messages/{values.MessageId}", cancellationToken);
        }

        internal async Task<Message> EditMessageAsync(EditMessageValues values, CancellationToken cancellationToken = default)
        {
            using var content = JsonContent.Create(values.ToBody(), options: Serialization.DefaultOptions);

            var response = await SendRequestAsync<JsonMessage>(HttpMethod.Patch, content, $"channels/{values.ChannelId}/messages/{values.MessageId}", cancellationToken);

            return Message.CreateNew(response, this);
        }

        internal async Task PinMessageAsync(PinMessageValues values, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync(HttpMethod.Post, $"channels/{values.ChannelId}/messages/{values.MessageId}/pin", cancellationToken);
        }

        internal async Task UnPinMessageAsync(PinMessageValues values, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync(HttpMethod.Delete, $"channels/{values.ChannelId}/messages/{values.MessageId}/pin", cancellationToken);
        }

        internal async Task<Message> GetMessageAsync(GetMessageValues values, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonMessage>(HttpMethod.Get, $"channels/{values.ChannelId}/messages/{values.MessageId}", cancellationToken);

            return Message.CreateNew(response, this);
        }
    }
}
