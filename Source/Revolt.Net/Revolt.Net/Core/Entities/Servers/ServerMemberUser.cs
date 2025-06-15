using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.JsonModels.Servers;
using Revolt.Net.Core.JsonModels.Users;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Servers
{
    public sealed class ServerMemberUser : RevoltClientEntity
    {
        internal JsonUser JsonUser;
        internal JsonServerMember JsonMember;

        internal ServerMemberUser(JsonUser user, JsonServerMember message, RevoltRestClient restClient) : base(restClient)
        {
            JsonUser = user;
            JsonMember = message;
        }

        public string ServerId => JsonMember.Id.ServerId;

        public string UserId => JsonMember.Id.UserId;

        public string? Nickname => JsonMember.Nickname;

        public DateTimeOffset JoinedAt => JsonMember.JoinedAt;

        public string[] Roles => JsonMember.Roles;

        public bool IsBot => JsonUser.Bot is not null;

        public string Username => JsonUser.Username;

        public string Discriminator => JsonUser.Discriminator;
    }
}
