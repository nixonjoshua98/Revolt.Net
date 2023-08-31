using Revolt.Net.Clients;

namespace Revolt.Net.Core.Entities.Common
{
    public abstract class RevoltEntity
    {
        private RevoltBotClient _Client = default!;

        internal RevoltBotClient Client =>
            _Client ?? throw new Exception("Entity does not have a client to use");

        internal void SetClient(RevoltBotClient client)
        {
            _Client = client;
        }
    }
}
