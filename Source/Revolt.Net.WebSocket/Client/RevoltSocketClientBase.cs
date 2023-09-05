using Revolt.Net.Core.Logging;

namespace Revolt.Net.WebSocket
{
    public abstract partial class RevoltSocketClientBase
    {
        private readonly LogManager LogManager;

        protected RevoltSocketClientBase()
        {
            LogManager = new(LogSeverity.Debug);
        }
    }
}
