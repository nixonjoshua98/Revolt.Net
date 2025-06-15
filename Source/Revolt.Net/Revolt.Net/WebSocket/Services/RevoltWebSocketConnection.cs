using Microsoft.Extensions.Logging;
using Revolt.Net.Core.Common;
using Revolt.Net.WebSocket.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Revolt.Net.WebSocket.Services
{
    internal sealed class RevoltWebSocketConnection(ILoggerFactory loggerFactory)
    {
        private readonly ILogger<RevoltWebSocketConnection> _logger = loggerFactory.CreateLogger<RevoltWebSocketConnection>();

        private readonly ClientWebSocket WebSocket = new();

        public async Task ConnectAsync(string websocketUri, CancellationToken cancellationToken)
        {
            var uri = new Uri(websocketUri);

            await WebSocket.ConnectAsync(uri, cancellationToken);

            _logger.LogDebug("Revolt.Net.WebSocket : Connected");
        }

        public async ValueTask SendAsync<T>(T message, CancellationToken cancellationToken) where T : class
        {
            var json = JsonSerializer.Serialize(message, Serialization.DefaultOptions);

            var bytes = Encoding.UTF8.GetBytes(json);

            await WebSocket.SendAsync(bytes, WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
        }

        public async Task<ReceiveWebSocketMessage> ReceiveAsync(byte[] buffer, CancellationToken cancellationToken)
        {
            var result = await WebSocket.ReceiveAsync(buffer, cancellationToken);

            var content = Encoding.UTF8.GetString(buffer, 0, result.Count);

            return new ReceiveWebSocketMessage(result.MessageType, content);
        }
    }
}
