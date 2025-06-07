using Microsoft.Extensions.DependencyInjection;

namespace Revolt.Net.Core.Hosting.Builders
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
