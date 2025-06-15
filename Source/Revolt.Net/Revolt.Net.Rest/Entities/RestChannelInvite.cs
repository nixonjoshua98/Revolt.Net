using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{
    internal class RestChannelInvite
    {
        internal JsonChannelInvite JsonModel;

        internal RestChannelInvite(JsonChannelInvite jsonModel)
        {
            JsonModel = jsonModel;
        }

        public string Id => JsonModel.Id;

        public string ServerId => JsonModel.ServerId;

        public string ChannelId => JsonModel.ChannelId;

        public string CreatorUserId => JsonModel.CreatorUserId;
    }
}
