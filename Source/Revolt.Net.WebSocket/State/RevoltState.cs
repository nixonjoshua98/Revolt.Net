using Revolt.Net.Rest;
using Revolt.Net.WebSocket.State.Messages;

namespace Revolt.Net.WebSocket.State
{
    internal sealed class RevoltState
    {
        private readonly RevoltSocketClient Client;
        private readonly RevoltApiClient Api;
        private readonly IRevoltStateCache Cache;

        public readonly MessagesState Messages;

        public RevoltState(RevoltSocketClient client)
        {
            Client = client;
            Cache = Client.Cache;
            Api = Client.Api;

            Messages = new(client);
        }

        public async ValueTask<User> GetUserAsync(string id, FetchBehaviour behaviour = FetchBehaviour.Cache)
        {
            return await RevoltStateHelper.GetOrDownloadAsync(
                behaviour, () => GetUser(id), () => Api.GetUserAsync(id), u => AddUser(u)
            );
        }

        public User GetUserByName(string name) => Cache.GetUserByName(name);

        public void Add(string id, ServerMembersResponse response)
        {
            AddUsers(response.Users);
            SetServerMembers(id, response.Members);
        }

        public User GetUser(string id)
        {
            var user = Cache.GetUser(id);
            return user;
        }

        public void Add(ReadyMessage @event)
        {
            AddUsers(@event.Users);
            AddServers(@event.Servers);
            AddChannels(@event.Channels);
            AddServerMembers(@event.Members);
        }

        public void AddServers(IEnumerable<Server> servers)
        {
            foreach (var server in servers) AddServer(server);
        }

        public void AddServer(Server server)
        {
            server.SetClient(Client);
            Cache.AddServer(server);
        }

        public void RemoveServer(string id) =>
            Cache.RemoveServer(id);

        public void RemoveChannel(string channelId) =>
            Cache.RemoveChannel(channelId);

        public void AddUser(User user) =>
            Cache.AddUser(user);

        public void AddUsers(IEnumerable<User> ls) =>
            Cache.AddUsers(ls);

        public void SetServerMembers(string id, IEnumerable<ServerMemberReference> ls) =>
            Cache.SetServerMembers(id, ls);

        public void AddServerMembers(IEnumerable<ServerMemberReference> ls) =>
            Cache.AddServerMembers(ls);

        public IEnumerable<ServerMember> GetServerMembers(string id) =>
            Cache.GetServerMembers(id);

        public ServerMember GetServerMember(string server, string userId) =>
            Cache.GetServerMember(server, userId);

        public void UpdateUser(string id, PartialUser partialUser) =>
            Cache.UpdateUser(id, partialUser);

        public Server GetServer(string id)
        {
            var server = Cache.GetServer(id);
            server?.SetClient(Client);
            return server;
        }

        public async ValueTask<Channel> GetChannelAsync(string id, FetchBehaviour behaviour = FetchBehaviour.Cache)
        {
            return await RevoltStateHelper.GetOrDownloadAsync(
                behaviour, () => GetChannel(id), () => Api.GetChannelAsync(id), c => AddChannel(c)
            );
        }

        public Channel GetChannel(string id)
        {
            var channel = Cache.GetChannel(id);
            channel?.SetClient(Client);
            return channel;
        }

        public void AddChannel(Channel channel)
        {
            channel?.SetClient(Client);
            Cache.AddChannel(channel);
        }

        public void AddChannels(IEnumerable<Channel> channels)
        {
            foreach (var chnl in channels) AddChannel(chnl);
        }
    }
}
