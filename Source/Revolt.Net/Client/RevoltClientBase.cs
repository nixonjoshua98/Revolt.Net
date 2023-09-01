using Revolt.Net.Logging;

namespace Revolt.Net.Client
{
    public abstract partial class RevoltClientBase
    {
        private readonly LogManager LogManager;

        protected RevoltClientBase()
        {
            LogManager = new(LogSeverity.Debug);
        }
    }
}
