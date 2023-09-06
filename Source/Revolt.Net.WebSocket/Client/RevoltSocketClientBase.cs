using Revolt.Net.Core.Logging;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.WebSocket
{
    public abstract partial class RevoltSocketClientBase : RevoltClientBase
    {
        private readonly LogManager LogManager;

        protected RevoltSocketClientBase(string apiUrl) : base(apiUrl)
        {
            LogManager = new(LogSeverity.Debug);
        }
    }
}
