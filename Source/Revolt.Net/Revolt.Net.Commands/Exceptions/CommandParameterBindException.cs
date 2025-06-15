using Revolt.Net.Core.Exceptions;

namespace Revolt.Net.Commands.Exceptions
{
    public sealed class CommandParameterBindException(string message) : RevoltException(message)
    {
        internal static CommandParameterBindException NoRegisteredType(Type nonBindableType) =>
            new CommandParameterBindException($"No registered binding for type '{nonBindableType.Name}' found.");

        internal static CommandParameterBindException BindingFailed(Type nonBindableType) =>
            new CommandParameterBindException($"Found binding for type '{nonBindableType.Name}' failed to parse type.");
    }

}
