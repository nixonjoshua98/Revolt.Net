using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket.State
{
    internal sealed class MessagesState
    {
        private readonly RevoltSocketClient Client;
        private readonly RevoltApiClient Api;
        private readonly IRevoltStateCache Cache;

        public MessagesState(RevoltSocketClient client)
        {
            Client = client;
            Cache = Client.Cache;
            Api = Client.Api;
        }

        public async Task<SocketMessage> GetAsync(string channel, string message, FetchBehaviour behaviour = FetchBehaviour.Cache)
        {
            return await RevoltStateHelper.GetOrDownloadAsync(
                behaviour, () => Get(channel, message), () => Api.GetMessageAsync(channel, message), m => Add(m));
        }

        public SocketMessage Get(string channelId, string messageId)
        {
            var message = Cache.GetMessage(channelId, messageId);
            message?.SetClient(Client);
            return message;
        }

        public async Task DeleteAsync(string channel, string message)
        {
            await Api.DeleteMessageAsync(channel, message);
            Remove(channel, message);
        }

        internal T TryAdd<T>(T message) where T : SocketMessage
        {
            if (message is not null)
            {
                Add(message);
            }

            return message;
        }

        public void Add(SocketMessage message)
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
