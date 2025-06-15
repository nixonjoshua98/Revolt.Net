using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.JsonModels.Users;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Users
{
    public sealed class User : RevoltClientEntity
    {
        internal JsonUser JsonModel;

        internal User(JsonUser user, RevoltRestClient client) : base(client)
        {
            JsonModel = user;
        }

        public string Id => JsonModel.Id;

        public bool IsBot => JsonModel.Bot is not null;

        public string Username => JsonModel.Username;

        public string Discriminator => JsonModel.Discriminator;
    }
}
