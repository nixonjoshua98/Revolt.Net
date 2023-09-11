using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket.State
{
    public interface IRevoltStateCache
    {
        void AddChannel(RestChannel channel);
        RestChannel GetChannel(string id);
        void AddMessage(SocketMessage message);
        void AddServer(RestServer server);
        void AddServerMembers(IEnumerable<ServerMember> ls);
        void AddUser(RestUser user);
        void AddUsers(IEnumerable<RestUser> ls);
        RestServer GetServer(string id);
        ServerMemberUser GetServerMember(string server, string userId);
        IEnumerable<ServerMemberUser> GetServerMembers(string id);
        RestUser GetUser(string id);
        void RemoveChannel(string channelId);
        void RemoveMessage(string channelId, string messageId);
        void RemoveServer(string id);
        void SetServerMembers(string id, IEnumerable<ServerMember> ls);
        void UpdateUser(string id, PartialUser partialUser);
        RestUser GetUserByName(string name);
        SocketMessage GetMessage(string channel, string message);
    }
}