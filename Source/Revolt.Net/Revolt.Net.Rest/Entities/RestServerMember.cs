using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{
    internal sealed class RestServerMember
    {
        private readonly JsonServerMember JsonModel;

        internal RestServerMember(JsonServerMember jsonModel)
        {
            JsonModel = jsonModel;
        }

        public string ServerId => JsonModel.Id.ServerId;

        public string UserId => JsonModel.Id.UserId;

        public string? Nickname => JsonModel.Nickname;

        public DateTimeOffset JoinedAt => JsonModel.JoinedAt;

        public string[] RoleIds => JsonModel.RoleIds;
    }
}
