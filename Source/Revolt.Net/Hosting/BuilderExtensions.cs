using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revolt.Net.Clients;
using Revolt.Net.Core;
using Revolt.Net.Core.Clients;
using Revolt.Net.Hosting.Services;
using Revolt.Net.State;

namespace Revolt.Net.Hosting
{
    public static class BuilderExtensions
    {
        public static IHostBuilder AddRevoltBotClient<TClient>(
            this IHostBuilder builder,
            TClient client,
            Action<HostBuilderContext, RevoltBotConfiguration> configurationFunc)
            where TClient : class, IRevolutClient
        {
            var config = new RevoltBotConfiguration();

            builder.ConfigureServices((context, services) =>
            {
                configurationFunc?.Invoke(context, config);

                services.AddSingleton<TClient>(client);
                services.AddSingleton<IRevolutClient>(provider => provider.GetRequiredService<TClient>());

                services.AddSingleton<IRevoltStateCache, DefaultRevoltStateCache>();

                services.AddSingleton(config);

                services.AddHostedService<RevoltClientExecutionService>();
            });

            return builder;
        }

        public static IHostBuilder AddRevoltBotClient(
            this IHostBuilder builder,
            Action<HostBuilderContext, RevoltBotConfiguration> configurationFunc)
        {
            return AddRevoltBotClient<RevoltClient>(builder, configurationFunc);
        }

            public static IHostBuilder AddRevoltBotClient<TClient>(
            this IHostBuilder builder,
            Action<HostBuilderContext, RevoltBotConfiguration> configurationFunc) 
            where TClient : class, IRevolutClient
        {
            var config = new RevoltBotConfiguration();

            builder.ConfigureServices((context, services) =>
            {
                configurationFunc?.Invoke(context, config);

                services.AddSingleton<TClient>();
                services.AddSingleton<IRevolutClient>(provider => provider.GetRequiredService<TClient>());

                services.AddSingleton<IRevoltStateCache, DefaultRevoltStateCache>();

                services.AddSingleton(config);

                services.AddHostedService<RevoltClientExecutionService>();
            });

            return builder;
        }
    }
}
