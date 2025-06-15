using Revolt.Net.Core.Common;
using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Contracts;
using Revolt.Net.Rest.Entities;
using System.Net.Http.Json;

namespace Revolt.Net.Rest.Clients
{
    internal partial class RevoltRestClient
    {
        public async Task<RestMessage> SendMessageAsync(SendMessageValues values, CancellationToken cancellationToken = default)
        {
            using var content = JsonContent.Create(values, options: Serialization.DefaultOptions);

            var response = await SendRequestAsync<JsonMessage>(HttpMethod.Post, content, $"channels/{values.ChannelId}/messages", cancellationToken);

            return RestMessage.CreateNew(response);
        }

        public async Task DeleteMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync(HttpMethod.Delete, $"channels/{channelId}/messages/{messageId}", cancellationToken);
        }

        public async Task<RestMessage> EditMessageAsync(EditMessageValues values, CancellationToken cancellationToken = default)
        {
            using var content = JsonContent.Create(values.ToBody(), options: Serialization.DefaultOptions);

            var response = await SendRequestAsync<JsonMessage>(HttpMethod.Patch, content, $"channels/{values.ChannelId}/messages/{values.MessageId}", cancellationToken);

            return RestMessage.CreateNew(response);
        }

        public async Task PinMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync(HttpMethod.Post, $"channels/{channelId}/messages/{messageId}/pin", cancellationToken);
        }

        public async Task UnPinMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync(HttpMethod.Delete, $"channels/{channelId}/messages/{messageId}/pin", cancellationToken);
        }

        public async Task<RestMessage> GetMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonMessage>(HttpMethod.Get, $"channels/{channelId}/messages/{messageId}", cancellationToken);

            return RestMessage.CreateNew(response);
        }
    }
}