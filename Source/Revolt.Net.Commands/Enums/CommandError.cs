using Revolt.Net.Commands._Original.Readers;
using Revolt.Net.Commands.Attributes;

namespace Revolt.Net.Commands.Enums
{
    /// <summary> 
    /// Defines the type of error a command can throw.
    /// </summary>
    public enum CommandError
    {
        /// <summary>
        /// Thrown when the command is unknown.
        /// </summary>
        UnknownCommand = 1,

        /// <summary>
        /// Thrown when the command fails to be parsed.
        /// </summary>
        ParseFailed,
        /// <summary>
        /// Thrown when the input text has too few or too many arguments.
        /// </summary>
        BadArgCount,

        /// <summary>
        /// Thrown when the object cannot be found by the <see cref="TypeReader"/>.
        /// </summary>
        ObjectNotFound,
        /// <summary>
        /// Thrown when more than one object is matched by <see cref="TypeReader"/>.
        /// </summary>
        MultipleMatches,

        /// <summary>
        /// Thrown when the command fails to meet a <see cref="PreconditionAttribute"/>'s conditions.
        /// </summary>
        UnmetPrecondition,

        /// <summary>
        /// Thrown when an exception occurs mid-command execution.
        /// </summary>
        Exception,

        /// <summary>
        /// Thrown when the command is not successfully executed on runtime.
        /// </summary>
        Unsuccessful
    }
}
