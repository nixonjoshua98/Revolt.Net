using Revolt.Net.Client;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Enums;
using Revolt.Net.Rest.ApiClient;

namespace Revolt.Net.State.Messages
{
    internal sealed class MessagesState
    {
        private readonly RevoltClient Client;
        private readonly RevoltApiClient Api;
        private readonly IRevoltStateCache Cache;

        public MessagesState(RevoltClient client)
        {
            Client = client;
            Cache = Client.Cache;
            Api = Client.Api;
        }

        public async Task<Message> GetAsync(string channel, string message, FetchBehaviour behaviour = FetchBehaviour.Cache)
        {
            return await RevoltStateHelper.GetOrDownloadAsync(
                behaviour, () => Get(channel, message), () => Api.GetMessageAsync(channel, message), m => Add(m));
        }

        public Message Get(string channelId, string messageId)
        {
            var message = Cache.GetMessage(channelId, messageId);
            message?.SetClient(Client); 
            return message;
        }

        public async Task<ClientMessage> SendAsync(string channel, string content)
        {
            var message = await Api.SendMessageAsync(channel, content);
            return TryAdd(message);
        }

        public async Task<ClientMessage> SendAsync(string channel, string messageId, string content)
        {
            var message = await Api.SendMessageAsync(channel, messageId, content);
            return TryAdd(message);
        }

        public async Task DeleteAsync(string channel, string message)
        {
            await Api.DeleteMessageAsync(channel, message);
            Remove(channel, message);
        }

        private T TryAdd<T>(T message) where T : Message
        {
            if (message is not null)
            {
                Add(message);
            }

            return message;
        }

        public void Add(Message message)
        {
            message?.SetClient(Client);
            Cache.AddMessage(message);
        }

        public void Remove(string channelId, string messageId)
        {
            Cache.RemoveMessage(channelId, messageId);
        }
    }
}
