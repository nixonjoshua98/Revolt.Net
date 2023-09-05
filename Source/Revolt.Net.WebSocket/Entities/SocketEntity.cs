namespace Revolt.Net.WebSocket
{
    public abstract class SocketEntity
    {
        private RevoltSocketClient _Client = default!;

        internal RevoltSocketClient Client =>
            _Client ?? throw new Exception("Entity does not have a client to use");

        internal void SetClient(RevoltSocketClient client)
        {
            _Client = client;
        }
    }
}
