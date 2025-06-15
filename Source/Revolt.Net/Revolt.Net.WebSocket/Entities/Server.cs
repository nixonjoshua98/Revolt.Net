using Revolt.Net.Rest.Entities;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public sealed class Server : RevoltSocketEntity
    {
        internal RestServer Model;

        internal Server(RestServer data, IRevoltWebSocketClient client) : base(client)
        {
            Model = data;
        }

        public string Id => Model.Id;

        public string OwnerId => Model.OwnerUserId;

        public string Name => Model.Name;

        public bool IsNSFW => Model.IsNSFW;

        public async Task<IReadOnlyList<ServerMemberUser>> GetMembersAsync(CancellationToken cancellationToken = default)
        {
            return await Client.GetServerMemberUsersAsync(Id, cancellationToken);
        }
    }
}
