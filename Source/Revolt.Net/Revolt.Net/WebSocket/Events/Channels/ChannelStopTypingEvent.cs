using Revolt.Net.WebSocket.JsonModels.Channels;

namespace Revolt.Net.WebSocket.Events
{
    public sealed record ChannelStopTypingEvent(string ChannelId, string UserId)
    {
        internal ChannelStopTypingEvent(JsonChannelStopTyping data) : this(data.Id, data.UserId)
        {

        }
    }
}
