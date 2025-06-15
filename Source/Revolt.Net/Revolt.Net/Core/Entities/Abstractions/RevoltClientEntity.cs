using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Abstractions
{
    public abstract class RevoltClientEntity
    {
        internal RevoltRestClient Client;

        internal RevoltClientEntity(RevoltRestClient client)
        {
            Client = client;
        }
    }
}
