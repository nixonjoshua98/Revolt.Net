using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket.State
{
    internal sealed class RevoltState
    {
        private readonly RevoltSocketClient Client;
        private readonly IRevoltStateCache Cache;

        public RevoltState(RevoltSocketClient client)
        {
            Client = client;
            Cache = Client.Cache;
        }

        #region Messages

        public void AddMessage(SocketMessage message)
        {
            message?.SetClient(Client);
            Cache.AddMessage(message);
        }

        public SocketMessage GetMessage(string channelId, string messageId)
        {
            var message = Cache.GetMessage(channelId, messageId);
            message?.SetClient(Client);
            return message;
        }

        public void RemoveMessage(string channelId, string messageId)
        {
            Cache.RemoveMessage(channelId, messageId);
        }

        public void RemoveMessage(RestMessage message)
        {
            RemoveMessage(message.ChannelId, message.Id);
        }

        #endregion

        public IUser GetUserByName(string name) => Cache.GetUserByName(name);

        public void Add(string id, ServerMembersResponse response)
        {
            AddUsers(response.Users);
            SetServerMembers(id, response.Members);
        }

        public RestUser GetUser(string id)
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

        public void AddServers(IEnumerable<RestServer> servers)
        {
            foreach (var server in servers) AddServer(server);
        }

        public void AddServer(RestServer server)
        {
            server.SetClient(Client);
            Cache.AddServer(server);
        }

        public void RemoveServer(string id) =>
            Cache.RemoveServer(id);

        public void RemoveChannel(string channelId) =>
            Cache.RemoveChannel(channelId);

        public void AddUser(RestUser user) =>
            Cache.AddUser(user);

        public void AddUsers(IEnumerable<RestUser> ls) =>
            Cache.AddUsers(ls);

        public void SetServerMembers(string id, IEnumerable<ServerMember> ls) =>
            Cache.SetServerMembers(id, ls);

        public void AddServerMembers(IEnumerable<ServerMember> ls) =>
            Cache.AddServerMembers(ls);

        public IEnumerable<ServerMemberUser> GetServerMembers(string id) =>
            Cache.GetServerMembers(id);

        public ServerMemberUser GetServerMember(string server, string userId) =>
            Cache.GetServerMember(server, userId);

        public void UpdateUser(string id, PartialUser partialUser) =>
            Cache.UpdateUser(id, partialUser);

        public RestServer GetServer(string id)
        {
            var server = Cache.GetServer(id);
            server?.SetClient(Client);
            return server;
        }

        public RestChannel GetChannel(string id)
        {
            var channel = Cache.GetChannel(id);
            channel?.SetClient(Client);
            return channel;
        }

        public void AddChannel(RestChannel channel)
        {
            channel?.SetClient(Client);
            Cache.AddChannel(channel);
        }

        public void AddChannels(IEnumerable<RestChannel> channels)
        {
            foreach (var chnl in channels) AddChannel(chnl);
        }
    }
}
