using System.Diagnostics.CodeAnalysis;

namespace Revolt.Net.Core.Exceptions
{
    public class RevoltException : Exception
    {
        public RevoltException(string message) : base(message)
        {

        }

        [return: NotNull]
        internal static T ThrowIfNull<T>(T? value, string entityName)
        {
            if (value is null)
            {
                throw new RevoltException($"Required value '{entityName}' was missing");
            }

            return value!;
        }
    }
}
