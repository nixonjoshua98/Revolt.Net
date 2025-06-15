using Revolt.Net.Core.Entities.Messages;

namespace Revolt.Net.WebSocket.Events
{
    public sealed record MessageReceivedEvent(Message Message);
}
