using Revolt.Net.Core.Entities.Messages;

namespace Revolt.Net.Commands.Extensions
{
    public static class MessageExtensions
    {
        public static bool HasStringPrefix(this Message msg, string str, out int argPos, StringComparison comparisonType = StringComparison.Ordinal)
        {
            argPos = -1;

            var text = msg.Content;

            if (!string.IsNullOrEmpty(text) && text.StartsWith(str, comparisonType))
            {
                argPos = str.Length;

                return true;
            }

            return false;
        }
    }
}
