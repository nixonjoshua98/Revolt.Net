using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Core.Builders;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.BackgroundServices;
using Revolt.Net.WebSocket.Services;

namespace Revolt.Net.WebSocket.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddWebSocket(this IRevoltBuilder builder)
        {
            builder.Services.AddHostedService<WebSocketBackgroundService>();

            builder.Services.TryAddSingleton<IRevoltWebSocketClient, RevoltWebSocketClient>();

            return builder;
        }
    }
}
