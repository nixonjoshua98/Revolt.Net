using Revolt.Net.WebSocket.JsonModels;

namespace Revolt.Net.WebSocket.Events
{
    public sealed record ChannelStartTypingEvent(string ChannelId, string UserId)
    {
        internal ChannelStartTypingEvent(JsonChannelStartTyping data) : this(data.Id, data.UserId)
        {

        }
    }
}
