using Revolt.Net.WebSocket.Entities;

namespace Revolt.Net.Commands.Extensions
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Checks if the message content starts with the specified prefix string.
        /// </summary>
        /// <param name="msg">The message to check.</param>
        /// <param name="str">The prefix string to look for.</param>
        /// <param name="argPos">
        /// When this method returns, contains the position immediately after the prefix if the prefix is found; otherwise, -1.
        /// This can be used as the starting index for parsing arguments after the prefix.
        /// </param>
        /// <param name="comparisonType">
        /// The type of string comparison to use. Defaults to <see cref="StringComparison.Ordinal"/>.
        /// </param>
        /// <returns>True if the message content starts with the specified prefix; otherwise, false.</returns>
        public static bool HasStringPrefix(this Message msg, string str, out int argPos, StringComparison comparisonType = StringComparison.Ordinal)
        {
            argPos = -1;

            var text = msg.Content;

            if (!string.IsNullOrEmpty(text) && text.StartsWith(str, comparisonType))
            {
                // The argument position is set to the length of the prefix, indicating
                // where the actual command arguments begin after the prefix.
                argPos = str.Length;
                return true;
            }

            return false;
        }

    }
}
