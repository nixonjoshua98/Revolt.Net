using Revolt.Net.Rest.Clients;

namespace Revolt.Net
{
    public abstract class RestEntity
    {
        private RevoltClientBase _Client = default!;

        internal RevoltClientBase Client
        {
            get => _Client ?? throw new Exception("Entity does not have a client to use");
            set => _Client = value;
        }

        internal void SetClient(RevoltClientBase client)
        {
            _Client = client;
        }
    }
}
