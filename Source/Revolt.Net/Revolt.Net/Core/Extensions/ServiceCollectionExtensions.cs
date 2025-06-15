using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Core.Builders;

namespace Revolt.Net.Core.Extensions
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
