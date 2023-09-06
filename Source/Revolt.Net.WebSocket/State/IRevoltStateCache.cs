namespace Revolt.Net.WebSocket.State
{
    public interface IRevoltStateCache
    {
        void AddChannel(SocketChannel channel);
        SocketChannel GetChannel(string id);
        void AddMessage(SocketMessage message);
        void AddServer(SocketServer server);
        void AddServerMembers(IEnumerable<ServerMemberReference> ls);
        void AddUser(SocketUser user);
        void AddUsers(IEnumerable<SocketUser> ls);
        SocketServer GetServer(string id);
        ServerMember GetServerMember(string server, string userId);
        IEnumerable<ServerMember> GetServerMembers(string id);
        SocketUser GetUser(string id);
        void RemoveChannel(string channelId);
        void RemoveMessage(string channelId, string messageId);
        void RemoveServer(string id);
        void SetServerMembers(string id, IEnumerable<ServerMemberReference> ls);
        void UpdateUser(string id, PartialUser partialUser);
        SocketUser GetUserByName(string name);
        SocketMessage GetMessage(string channel, string message);
    }
}