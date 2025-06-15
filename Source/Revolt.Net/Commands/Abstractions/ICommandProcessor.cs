using Revolt.Net.Commands.Models;

namespace Revolt.Net.Commands.Abstractions
{
    public interface ICommandProcessor
    {
        Task<CommandExecutionResult> ExecuteAsync(CommandContext context, int commandStartIndex, CancellationToken cancellationToken);
    }
}