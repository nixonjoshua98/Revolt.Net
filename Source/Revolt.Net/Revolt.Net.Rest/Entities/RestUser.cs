using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{
    internal sealed class RestUser
    {
        internal JsonUser JsonModel;

        internal RestUser(JsonUser jsonModel)
        {
            JsonModel = jsonModel;
        }

        public string Id => JsonModel.Id;
        public string Username => JsonModel.Username;
        public bool IsBot => JsonModel.Bot is not null;
        public string Discriminator => JsonModel.Discriminator;
    }
}
