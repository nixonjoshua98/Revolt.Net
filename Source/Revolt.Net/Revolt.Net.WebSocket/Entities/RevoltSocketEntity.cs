using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public abstract class RevoltSocketEntity
    {
        internal IRevoltWebSocketClient Client;

        internal RevoltSocketEntity(IRevoltWebSocketClient client)
        {
            Client = client;
        }
    }
}
