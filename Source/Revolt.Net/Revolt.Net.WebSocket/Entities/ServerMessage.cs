using Revolt.Net.Core.Exceptions;
using Revolt.Net.Core.JsonModels;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public sealed class ServerMessage : Message
    {
        internal ServerMessage(JsonMessage messageData, Server server, IRevoltWebSocketClient client) : base(messageData, client)
        {
            Author = new ServerMemberUser(
                RevoltException.ThrowIfNull(messageData.User, nameof(messageData.User)),
                RevoltException.ThrowIfNull(messageData.Member, nameof(messageData.Member)),
                client
            );

            Server = server;
        }

        public string ServerId => Author.ServerId;

        public Server Server { get; init; }

        public new ServerMemberUser Author { get; init; }
    }
}
