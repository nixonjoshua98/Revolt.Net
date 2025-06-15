namespace Revolt.Net.Commands.Checks
{
    /// <summary>
    /// Marker interface for all command pre-check types.
    /// </summary>
    /// <remarks>
    /// Pre-checks are used to validate whether a command can be executed in a given context.
    /// This interface is typically implemented by attributes or custom metadata used in command filtering.
    /// </remarks>
    public interface ICommandPreCheck;

    /// <summary>
    /// Base class for defining attribute-based command pre-checks.
    /// </summary>
    /// <remarks>
    /// Derive from this class to implement custom command validation logic via attributes.
    /// These are processed by the command system before a command executes.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class CommandPreCheckAttribute : Attribute, ICommandPreCheck;

    /// <summary>
    /// Defines a handler that performs logic to validate a specific type of command pre-check.
    /// </summary>
    /// <typeparam name="TCheck">The type of pre-check being handled, implementing <see cref="ICommandPreCheck"/>.</typeparam>
    public interface ICommandPreCheckHandler<TCheck> where TCheck : ICommandPreCheck
    {
        /// <summary>
        /// Executes the validation logic for the given pre-check and command context.
        /// </summary>
        /// <param name="context">The context in which the command is being executed.</param>
        /// <param name="check">The specific pre-check instance to validate.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="CommandPreCheckResult"/> indicating success or failure, with an optional message.</returns>
        Task<CommandPreCheckResult> CheckAsync(CommandContext context, TCheck check, CancellationToken cancellationToken);
    }

}
