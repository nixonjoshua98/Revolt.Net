using Revolt.Net.Commands.Abstractions;
using Revolt.Net.WebSocket.Models.Messages;

namespace Revolt.Net.Commands
{
    public sealed record CommandContext(SocketMessage Message) : ICommandContext;
}
