using Revolt.Net.WebSocket.JsonModels.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.Events.Channels
{
    public sealed record ChannelStartTypingEvent(string ChannelId, string UserId)
    {
        internal ChannelStartTypingEvent(JsonChannelStartTyping data) : this(data.Id, data.UserId)
        {
            
        }
    }
}
