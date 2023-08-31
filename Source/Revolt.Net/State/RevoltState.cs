﻿using Revolt.Net.Clients;
using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Core.Entities.Users.Partials;
using Revolt.Net.Core.Enums;
using Revolt.Net.Rest.API;
using Revolt.Net.Rest.API.Responses;
using Revolt.Net.Websocket.Events.Incoming;

namespace Revolt.Net.State
{
    internal sealed class RevoltState
    {
        private readonly RevoltClient Client;
        private readonly RevoltApiClient Api;
        private readonly IRevoltStateCache Cache;

        public RevoltState(RevoltClient client)
        {
            Client = client;
            Cache = Client.Cache;
            Api = Client.Api;
        }

        public void Add(string id, ServerMembersResponse response)
        {
            Cache.AddUsers(response.Users);
            Cache.SetServerMembers(id, response.Members);
        }

        public Channel? GetChannel(string id)
        {
            var channel = Cache.GetChannel(id);
            return channel;
        }

        public void AddChannel(Channel channel)
        {
            Cache.AddChannel(channel);
        }

        public async ValueTask<Channel?> GetChannelAsync(string id, FetchBehaviour behaviour = FetchBehaviour.Cache)
        {
            return await RevoltStateHelper.GetOrDownloadAsync(
                behaviour, () => GetChannel(id), () => Api.GetChannelAsync(id), c => AddChannel(c)
            );
        }

        public User? GetUser(string id)
        {
            var user = Cache.GetUser(id);
            return user;
        }

        public async Task<User?> GetUserAsync(string id, FetchBehaviour behaviour = FetchBehaviour.Cache)
        {
            return await RevoltStateHelper.GetOrDownloadAsync(
                behaviour, () => GetUser(id), () => Api.GetUserAsync(id), u => AddUser(u)
            );
        }

        public void Add(ReadyInternalEvent @event)
        {
            AddServers(@event.Servers);

            Cache.AddUsers(@event.Users);
            Cache.AddChannels(@event.Channels);
            Cache.AddServerMembers(@event.Members);
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

        public void AddChannels(IEnumerable<Channel> ls) =>
            Cache.AddChannels(ls);

        public void AddMessage(Message message)
        {
            message.SetClient(Client);
            Cache.AddMessage(message);
        }

        public void RemoveMessage(string channelId, string messageId) =>
            Cache.RemoveMessage(channelId, messageId);

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

        public ServerMember? GetServerMember(string server, string userId) =>
            Cache.GetServerMember(server, userId);

        public void UpdateUser(string id, PartialUser partialUser) =>
            Cache.UpdateUser(id, partialUser);

        public Server? GetServer(string id)
        {
            var server = Cache.GetServer(id);
            server?.SetClient(Client);
            return server;
        }
    }
}
