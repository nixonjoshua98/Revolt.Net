using Revolt.Net.Core.JsonModels;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public sealed class ServerMemberUser : RevoltSocketEntity
    {
        internal JsonUser JsonUser;
        internal JsonServerMember JsonMember;

        internal ServerMemberUser(JsonUser user, JsonServerMember message, IRevoltWebSocketClient client) : base(client)
        {
            JsonUser = user;
            JsonMember = message;
        }

        public string ServerId => JsonMember.Id.ServerId;

        public string UserId => JsonMember.Id.UserId;

        public string? Nickname => JsonMember.Nickname;

        public DateTimeOffset JoinedAt => JsonMember.JoinedAt;

        public string[] RoleIds => JsonMember.RoleIds;

        public bool IsBot => JsonUser.Bot is not null;

        public string Username => JsonUser.Username;

        public string Discriminator => JsonUser.Discriminator;
    }
}
