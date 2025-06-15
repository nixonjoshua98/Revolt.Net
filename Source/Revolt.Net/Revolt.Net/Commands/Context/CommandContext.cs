using Revolt.Net.Core.Entities.Messages;
using System.Text.RegularExpressions;

namespace Revolt.Net.Commands
{
    /// <summary>
    /// Represents the context in which a command is executed, including the source message and parsed arguments.
    /// </summary>
    /// <param name="Message">The original message that triggered the command.</param>
    public sealed partial record CommandContext(Message Message)
    {
        /// <summary>
        /// Parses the message content starting from the given index and returns a list of arguments.
        /// </summary>
        /// <param name="commandStartIndex">The index in the message where command arguments begin.</param>
        /// <returns>A list of parsed arguments. Supports quoted strings and space-separated tokens.</returns>
        internal List<string> GetArguments(int commandStartIndex)
        {
            var matches = CommandParserRegex().Matches(Message.Content ?? string.Empty, commandStartIndex);

            // Extract either the quoted group or the regular token group
            return [.. matches.Select(m => m.Groups[1].Success ? m.Groups[1].Value : m.Groups[2].Value)];
        }

        /// <summary>
        /// Regex pattern to match either:
        /// - Single-quoted arguments (e.g., 'some value')
        /// - Or unquoted, non-whitespace sequences (e.g., word)
        /// </summary>
        /// <returns>A compiled regular expression instance.</returns>
        [GeneratedRegex(@"'([^']*)'|(\S+)")]
        private static partial Regex CommandParserRegex();
    }

}
