namespace Revolt.Net.Commands.Models
{
    internal sealed record InvokableCommand(
        RevoltCommandDescriptor CommandDescriptor, 
        object?[] Parameters
    );
}
