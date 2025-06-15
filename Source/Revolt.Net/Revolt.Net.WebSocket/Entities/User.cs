using Revolt.Net.Core.JsonModels;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public sealed class User : RevoltSocketEntity
    {
        internal JsonUser JsonModel;

        internal User(JsonUser user, IRevoltWebSocketClient client) : base(client)
        {
            JsonModel = user;
        }

        public string Id => JsonModel.Id;

        public bool IsBot => JsonModel.Bot is not null;

        public string Username => JsonModel.Username;

        public string Discriminator => JsonModel.Discriminator;
    }
}
