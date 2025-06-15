using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Core.Builders;
using Revolt.Net.Core.Configuration;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Rest.Hosting.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddRestClient(this IRevoltBuilder builder, string serverUrl, string token)
        {
            builder.Services.Configure<RevoltConfiguration>(cfg =>
            {
                cfg.ServerUrl = new Uri(serverUrl);
                cfg.Token = token;
            });

            builder.Services.AddHttpClient();

            builder.Services.TryAddSingleton<RevoltRestClient>();

            return builder;
        }
    }
}
