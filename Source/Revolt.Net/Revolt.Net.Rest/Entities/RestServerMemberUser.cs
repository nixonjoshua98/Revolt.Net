using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{
    internal sealed class RestServerMemberUser
    {
        internal JsonUser JsonUser;
        internal JsonServerMember JsonMember;

        internal RestServerMemberUser(JsonUser user, JsonServerMember message)
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
