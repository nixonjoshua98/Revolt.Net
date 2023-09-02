namespace Revolt.Net.Core.Exceptions
{
    public class RevoltException : Exception
    {
        public RevoltException() : base()
        {

        }

        public RevoltException(string message) : base(message)
        {

        }

        public RevoltException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
