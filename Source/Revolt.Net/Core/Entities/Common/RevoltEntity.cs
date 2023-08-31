using Revolt.Net.Clients;

namespace Revolt.Net.Core.Entities.Common
{
    public abstract class RevoltEntity
    {
        private RevoltClient _Client = default!;

        internal RevoltClient Client =>
            _Client ?? throw new Exception("Entity does not have a client to use");

        internal void SetClient(RevoltClient client)
        {
            _Client = client;
        }
    }
}
