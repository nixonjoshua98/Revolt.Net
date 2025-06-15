using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Revolt.Net.Core.Builders;
using Revolt.Net.WebSocket.BackgroundServices;
using Revolt.Net.WebSocket.Client;
using Revolt.Net.WebSocket.Services;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket.Extensions
{
    public static class IRevoltBuilderExtensions
    {
        public static IRevoltBuilder AddWebSocket(this IRevoltBuilder builder)
        {
            builder.Services.TryAddSingleton<RevoltClientState>();

            builder.Services.AddHostedService<WebSocketBackgroundService>();

            builder.Services.AddHostedService<WebSocketEventHandler>();

            builder.Services.TryAddSingleton<IRevoltWebSocketClient, RevoltWebSocketClient>();

            return builder;
        }
    }
}
