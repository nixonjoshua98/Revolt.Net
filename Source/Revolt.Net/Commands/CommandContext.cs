using Revolt.Net.Commands.Abstractions;
using Revolt.Net.WebSocket.Entities.Messages;

namespace Revolt.Net.Commands
{
    public sealed record CommandContext(SocketMessage Message) : ICommandContext;
}
