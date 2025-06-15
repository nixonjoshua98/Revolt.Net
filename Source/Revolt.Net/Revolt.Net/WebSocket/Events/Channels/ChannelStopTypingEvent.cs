using Revolt.Net.WebSocket.JsonModels.Channels;

namespace Revolt.Net.WebSocket.Events.Channels
{
    public sealed record ChannelStopTypingEvent(string ChannelId, string UserId)
    {
        internal ChannelStopTypingEvent(JsonChannelStopTyping data) : this(data.Id, data.UserId)
        {

        }
    }
}
