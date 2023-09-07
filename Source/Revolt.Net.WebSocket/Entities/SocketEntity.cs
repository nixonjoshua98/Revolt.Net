namespace Revolt.Net.WebSocket
{
    public abstract class SocketEntity
    {
        private RevoltSocketClient _Client = default!;

        internal RevoltSocketClient Client
        {
            get => _Client ?? throw new Exception("Entity does not have a client to use");
            set => _Client = value;
        }

        internal void SetClient(RevoltSocketClient client)
        {
            _Client = client;
        }
    }
}
