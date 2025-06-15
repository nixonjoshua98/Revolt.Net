using Revolt.Net.Core.JsonModels;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public sealed class ServerMember : RevoltSocketEntity
    {
        internal JsonServerMember JsonModel;

        internal ServerMember(JsonServerMember data, IRevoltWebSocketClient client) : base(client)
        {
            JsonModel = data;
        }

        public string ServerId => JsonModel.Id.ServerId;

        public string UserId => JsonModel.Id.UserId;

        public string? Nickname => JsonModel.Nickname;

        public DateTimeOffset JoinedAt => JsonModel.JoinedAt;

        public string[] RoleIds => JsonModel.RoleIds;

        public async Task<ServerMemberUser> GetUserAsync(CancellationToken cancellationToken = default)
        {
            var user = await Client.GetUserAsync(UserId, cancellationToken);

            return new ServerMemberUser(user.JsonModel, JsonModel, Client);
        }
    }
}
