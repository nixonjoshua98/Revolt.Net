using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.JsonModels.Channels;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Channels
{
    public sealed class ChannelInvite : RevoltClientEntity
    {
        internal JsonChannelInvite JsonModel;

        internal ChannelInvite(JsonChannelInvite data, RevoltRestClient client) : base(client)
        {
            JsonModel = data;
        }

        public string Id => JsonModel.Id;

        public string ServerId => JsonModel.ServerId;

        public string ChannelId => JsonModel.ChannelId;

        public string CreatorUserId => JsonModel.CreatorUserId;
    }
}
