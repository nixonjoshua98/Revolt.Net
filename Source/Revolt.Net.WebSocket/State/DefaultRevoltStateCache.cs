﻿using Revolt.Net.Rest;
using System.Collections.Concurrent;

namespace Revolt.Net.WebSocket.State
{
    internal sealed class DefaultRevoltStateCache : IRevoltStateCache
    {
        private readonly ConcurrentDictionary<string, RestUser> Users = new();
        private readonly ConcurrentDictionary<string, SocketServer> Servers = new();
        private readonly ConcurrentDictionary<string, RestChannel> Channels = new();

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, SocketMessage>> Messages = new();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ServerMemberReference>> ServerMemberRefs = new();

        public SocketMessage GetMessage(string channel, string message)
        {
            return GetChannelMessages(channel).GetValueOrDefault(message);
        }

        public void AddServer(SocketServer server)
        {
            Servers[server.Id] = server;
        }

        public RestChannel GetChannel(string id)
        {
            return Channels.GetValueOrDefault(id);
        }

        public void RemoveServer(string id)
        {
            ServerMemberRefs.Remove(id, out _);

            if (Servers.TryRemove(id, out var server))
            {
                RemoveChannels(server.ChannelIds);
            }
        }

        private void RemoveChannels(IEnumerable<string> channelIds)
        {
            foreach (var channelId in channelIds)
            {
                RemoveChannel(channelId);
            }
        }

        public void RemoveChannel(string channelId)
        {
            Channels.Remove(channelId, out var _);
            Messages.Remove(channelId, out var _);
        }

        public void AddChannel(RestChannel channel)
        {
            Channels[channel.Id] = channel;
        }

        public void AddMessage(SocketMessage message)
        {
            GetChannelMessages(message.Channel.Id)[message.Id] = message;
        }

        public void RemoveMessage(string channelId, string messageId) =>
            GetChannelMessages(channelId).TryRemove(messageId, out var _);

        public RestUser GetUserByName(string name)
        {
            return Users.Values
                .FirstOrDefault(user =>
                    string.Equals(name, user.Username, StringComparison.OrdinalIgnoreCase));
        }

        public void AddUser(RestUser user)
        {
            Users[user.Id] = user;
        }

        public void AddUsers(IEnumerable<RestUser> ls)
        {
            foreach (var ele in ls)
            {
                Users[ele.Id] = ele;
            }
        }

        public void SetServerMembers(string id, IEnumerable<ServerMemberReference> ls)
        {
            ServerMemberRefs.Remove(id, out _);

            AddServerMembers(ls);
        }

        public void AddServerMembers(IEnumerable<ServerMemberReference> ls)
        {
            foreach (var ele in ls)
            {
                var server = GetServerMembersRefDict(ele.ServerId);

                server[ele.UserId] = ele;
            }
        }

        public IEnumerable<ServerMember> GetServerMembers(string id)
        {
            foreach (var userRef in GetServerMemberRefs(id))
            {
                var member = GetServerMember(id, userRef.UserId);

                if (member is not null)
                {
                    yield return member;
                }
            }
        }

        public ServerMember GetServerMember(string server, string userId)
        {
            var memberRef = GetServerMemberRef(server, userId);

            if (memberRef is not null)
            {
                var user = GetUser(userId);

                return user is null ?
                    default : ServerMember.Create(memberRef, user);
            }

            return default!;
        }

        public void UpdateUser(string id, PartialUser partialUser)
        {
            var user = GetUser(id);
            user?.UpdateFromPartial(partialUser);
        }

        public SocketServer GetServer(string id) =>
            Servers.GetValueOrDefault(id);

        public RestUser GetUser(string id) =>
            Users.GetValueOrDefault(id);

        private IEnumerable<ServerMemberReference> GetServerMemberRefs(string id) =>
            ServerMemberRefs.TryGetValue(id, out var server) ? server.Values : Enumerable.Empty<ServerMemberReference>();

        private ConcurrentDictionary<string, ServerMemberReference> GetServerMembersRefDict(string id) =>
            ServerMemberRefs.GetOrAdd(id, key => new());

        private ConcurrentDictionary<string, SocketMessage> GetChannelMessages(string id) =>
            Messages.GetOrAdd(id, key => new());

        private ServerMemberReference GetServerMemberRef(string serverId, string userId) =>
            ServerMemberRefs.TryGetValue(serverId, out var server) ? server.GetValueOrDefault(userId) : default;
    }
}
