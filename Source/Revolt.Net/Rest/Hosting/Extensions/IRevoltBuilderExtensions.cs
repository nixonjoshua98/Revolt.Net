using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Core.Hosting.Builders;
using Revolt.Net.Core.Hosting.Configuration;
using Revolt.Net.Rest.Common;

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

            builder.Services.AddHttpClient<RevoltApiClient>(client =>
            {
                client.BaseAddress = new Uri(serverUrl);

                client.DefaultRequestHeaders.Remove(RevoltRestConstant.BotTokenHeader);

                client.DefaultRequestHeaders.Add(RevoltRestConstant.BotTokenHeader, token);
            });

            return builder;
        }
    }
}
