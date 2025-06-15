namespace Revolt.Net.Commands.Models
{
    internal sealed record InvokableCommand(RegisteredCommand RegisteredCommand, object?[] Parameters);
}
