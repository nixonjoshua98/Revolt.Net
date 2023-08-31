using Revolt.Net.Core.Entities.Messages;

namespace Revolt.Net.Websocket.Events.Incoming
{
    public sealed record MessageEvent(Message Message);
}
