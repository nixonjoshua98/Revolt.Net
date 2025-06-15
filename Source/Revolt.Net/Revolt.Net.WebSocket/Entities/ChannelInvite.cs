using Revolt.Net.Core.JsonModels;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public sealed class ChannelInvite : RevoltSocketEntity
    {
        internal JsonChannelInvite JsonModel;

        internal ChannelInvite(JsonChannelInvite data, IRevoltWebSocketClient client) : base(client)
        {
            JsonModel = data;
        }

        public string Id => JsonModel.Id;

        public string ServerId => JsonModel.ServerId;

        public string ChannelId => JsonModel.ChannelId;

        public string CreatorUserId => JsonModel.CreatorUserId;
    }
}
