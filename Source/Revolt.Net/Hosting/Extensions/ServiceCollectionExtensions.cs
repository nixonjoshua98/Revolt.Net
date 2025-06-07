using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Hosting.Builders;

namespace Revolt.Net.Hosting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRevolt(this IServiceCollection services, Action<IRevoltBuilder> action)
        {
            var builder = new RevoltBuilder(services);

            action(builder);

            return services;
        }
    }
}
