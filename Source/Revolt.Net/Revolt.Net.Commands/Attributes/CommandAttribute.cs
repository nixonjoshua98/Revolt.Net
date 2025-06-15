using Revolt.Net.Commands.Abstractions;

namespace Revolt.Net.Commands.Attributes
{
    /// <summary>
    /// Marks a method as a command that can be executed by the Revolt command processor.
    /// </summary>
    /// <param name="name">The name or trigger used to invoke the command (e.g., "ping", "help").</param>
    /// <param name="priority">
    /// The priority of the command. Commands with higher priority are matched first when multiple candidates exist.
    /// Defaults to 0.
    /// </param>
    /// <remarks>
    /// This attribute should be applied to methods within classes derived from <see cref="RevoltCommandModule"/>.
    /// The command processor uses this metadata to discover and invoke appropriate command handlers.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandAttribute(string name, int priority = 0) : Attribute
    {
        /// <summary>
        /// Gets the name of the command used to invoke it.
        /// </summary>
        public readonly string Name = name;

        /// <summary>
        /// Gets the priority of the command for matching purposes.
        /// Higher values indicate higher precedence.
        /// </summary>
        public readonly int Priority = priority;
    }

}
