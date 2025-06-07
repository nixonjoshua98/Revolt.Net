using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Core.Hosting.Builders;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Hosting.HostedServices;
using Revolt.Net.WebSocket.Services;

namespace Revolt.Net.WebSocket.Hosting.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddWebSocket(this IRevoltBuilder builder)
        {
            builder.Services.AddHostedService<WebSocketBackgroundService>();

            builder.Services.TryAddSingleton<IWebSocketEventHub, WebSocketEventHub>();

            return builder;
        }
    }
}
