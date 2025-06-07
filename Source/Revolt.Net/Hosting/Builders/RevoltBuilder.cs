using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Hosting.Configuration;

namespace Revolt.Net.Hosting.Builders
{
    public interface IRevoltBuilder
    {
        internal IServiceCollection Services { get; }
    }

    internal sealed class RevoltBuilder(IServiceCollection serviceCollection) : IRevoltBuilder
    {
        public IServiceCollection Services { get; } = serviceCollection;
    }
}
