using Revolt.Net.Client;

namespace Revolt.Net.Entities.Common
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
