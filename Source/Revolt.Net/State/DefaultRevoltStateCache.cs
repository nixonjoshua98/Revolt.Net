using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Entities.Servers;
using Revolt.Net.Entities.Users;
using Revolt.Net.Entities.Users.Partials;
using System.Collections.Concurrent;

namespace Revolt.Net.State
{
    internal sealed class DefaultRevoltStateCache : IRevoltStateCache
    {
        private readonly ConcurrentDictionary<string, User> Users = new();
        private readonly ConcurrentDictionary<string, Server> Servers = new();
        private readonly ConcurrentDictionary<string, Channel> Channels = new();

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Message>> Messages = new();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ServerMemberReference>> ServerMemberRefs = new();

        public Message GetMessage(string channel, string message)
        {
            return GetChannelMessages(channel).GetValueOrDefault(message);
        }

        public void AddServer(Server server)
        {
            Servers[server.Id] = server;
        }

        public Channel GetChannel(string id)
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

        public void AddChannel(Channel channel)
        {
            Channels[channel.Id] = channel;
        }

        public void AddMessage(Message message)
        {
            GetChannelMessages(message.ChannelId)[message.Id] = message;
        }

        public void RemoveMessage(string channelId, string messageId) =>
            GetChannelMessages(channelId).TryRemove(messageId, out var _);

        public User GetUserByName(string name)
        {
            return Users.Values
                .FirstOrDefault(user =>
                    string.Equals(name, user.Username, StringComparison.OrdinalIgnoreCase));
        }

        public void AddUser(User user)
        {
            Users[user.Id] = user;
        }

        public void AddUsers(IEnumerable<User> ls)
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

            user.UpdateFromPartial(partialUser);
        }

        public Server GetServer(string id) =>
            Servers.GetValueOrDefault(id);

        public User GetUser(string id) =>
            Users.GetValueOrDefault(id);

        private IEnumerable<ServerMemberReference> GetServerMemberRefs(string id) =>
            ServerMemberRefs.TryGetValue(id, out var server) ? server.Values : Enumerable.Empty<ServerMemberReference>();

        private ConcurrentDictionary<string, ServerMemberReference> GetServerMembersRefDict(string id) =>
            ServerMemberRefs.GetOrAdd(id, key => new());

        private ConcurrentDictionary<string, Message> GetChannelMessages(string id) =>
            Messages.GetOrAdd(id, key => new());

        private ServerMemberReference GetServerMemberRef(string serverId, string userId) =>
            ServerMemberRefs.TryGetValue(serverId, out var server) ? server.GetValueOrDefault(userId) : default;
    }
}
