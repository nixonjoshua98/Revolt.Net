using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.JsonModels.Servers;
using Revolt.Net.Core.JsonModels.Users;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Servers
{
    public sealed class ServerMember : RevoltClientEntity
    {
        internal JsonServerMember JsonModel;

        internal ServerMember(JsonServerMember data, RevoltRestClient restClient) : base(restClient)
        {
            JsonModel = data;
        }

        public string ServerId => JsonModel.Id.ServerId;

        public string UserId => JsonModel.Id.UserId;

        public string? Nickname => JsonModel.Nickname;

        public DateTimeOffset JoinedAt => JsonModel.JoinedAt;

        public string[] Roles => JsonModel.Roles;
    }
}
