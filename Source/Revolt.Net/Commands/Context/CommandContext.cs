using Revolt.Net.Core.Entities.Messages;
using System.Text.RegularExpressions;

namespace Revolt.Net.Commands
{
    public sealed partial record CommandContext(Message Message)
    {
        internal List<string> GetArguments(int commandStartIndex)
        {
            var matches = CommandParserRegex().Matches(Message.Content ?? string.Empty, commandStartIndex);

            return [.. matches.Select(m => m.Groups[1].Success ? m.Groups[1].Value : m.Groups[2].Value)];
        }

        [GeneratedRegex(@"'([^']*)'|(\S+)")]
        private static partial Regex CommandParserRegex();
    }
}
