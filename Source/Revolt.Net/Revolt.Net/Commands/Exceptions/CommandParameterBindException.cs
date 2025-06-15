using Revolt.Net.Core.Exceptions;

namespace Revolt.Net.Commands.Exceptions
{
    /// <summary>
    /// Exception thrown when binding command parameters fails.
    /// </summary>
    /// <param name="message">The error message describing the binding failure.</param>
    public sealed class CommandParameterBindException(string message) : RevoltException(message)
    {
        /// <summary>
        /// Creates an exception indicating that no registered binding exists for the specified type.
        /// </summary>
        /// <param name="nonBindableType">The type for which no binding was found.</param>
        /// <returns>A new <see cref="CommandParameterBindException"/> instance.</returns>
        internal static CommandParameterBindException NoRegisteredType(Type nonBindableType) =>
            new CommandParameterBindException($"No registered binding for type '{nonBindableType.Name}' found.");

        /// <summary>
        /// Creates an exception indicating that a binding was found but failed to parse the specified type.
        /// </summary>
        /// <param name="nonBindableType">The type that failed to be parsed by the binding.</param>
        /// <returns>A new <see cref="CommandParameterBindException"/> instance.</returns>
        internal static CommandParameterBindException BindingFailed(Type nonBindableType) =>
            new CommandParameterBindException($"Found binding for type '{nonBindableType.Name}' failed to parse type.");
    }

}
