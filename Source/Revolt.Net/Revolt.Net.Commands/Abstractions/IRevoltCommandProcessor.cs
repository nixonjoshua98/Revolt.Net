using Revolt.Net.Commands.Models;

namespace Revolt.Net.Commands.Abstractions
{
    /// <summary>
    /// Defines a contract for processing commands received in the context of a Revolt bot or client.
    /// </summary>
    public interface IRevoltCommandProcessor
    {
        /// <summary>
        /// Asynchronously executes a command based on the given context.
        /// </summary>
        /// <param name="context">The context in which the command is being executed, including user, message, and environment data.</param>
        /// <param name="commandStartIndex">The starting index in the message content where the actual command begins.</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete, allowing the operation to be cancelled.</param>
        /// <returns>A task representing the asynchronous operation, containing the result of the command execution.</returns>
        Task<CommandExecutionResult> ExecuteAsync(CommandContext context, int commandStartIndex, CancellationToken cancellationToken);
    }

}