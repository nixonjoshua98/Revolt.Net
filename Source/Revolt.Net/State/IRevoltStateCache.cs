using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Entities.Servers;
using Revolt.Net.Entities.Users;
using Revolt.Net.Entities.Users.Partial;

namespace Revolt.Net.State
{
    public interface IRevoltStateCache
    {
        void AddChannel(Channel channel);
        Channel GetChannel(string id);
        void AddMessage(Message message);
        void AddServer(Server server);
        void AddServerMembers(IEnumerable<ServerMemberReference> ls);
        void AddUser(User user);
        void AddUsers(IEnumerable<User> ls);
        Server GetServer(string id);
        ServerMember GetServerMember(string server, string userId);
        IEnumerable<ServerMember> GetServerMembers(string id);
        User GetUser(string id);
        void RemoveChannel(string channelId);
        void RemoveMessage(string channelId, string messageId);
        void RemoveServer(string id);
        void SetServerMembers(string id, IEnumerable<ServerMemberReference> ls);
        void UpdateUser(string id, PartialUser partialUser);
        User GetUserByName(string name);
        Message GetMessage(string channel, string message);
    }
}