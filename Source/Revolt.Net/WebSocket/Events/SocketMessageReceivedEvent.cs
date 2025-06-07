using Revolt.Net.Core.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.Events
{
    public sealed record SocketMessageReceivedEvent(Message Message);
}
