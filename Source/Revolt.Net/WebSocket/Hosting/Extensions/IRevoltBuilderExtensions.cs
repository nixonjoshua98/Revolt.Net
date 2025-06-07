using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Hosting.Builders;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Hosting.HostedServices;
using Revolt.Net.WebSocket.Services;

namespace Revolt.Net.WebSocket.Hosting.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddWebSocketService(this IRevoltBuilder builder)
        {
            builder.Services.AddHostedService<RevoltWebSocketBackgroundService>();

            builder.Services.TryAddSingleton<IWebSocketEventHub, WebSocketEventHub>();

            return builder;
        }
    }
}
