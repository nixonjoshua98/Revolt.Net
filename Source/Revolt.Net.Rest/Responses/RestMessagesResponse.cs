using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Rest
{
    public sealed class RestMessagesResponse
    {
        public IEnumerable<RestMessage> Messages { get; init; }
        public IEnumerable<RestUser> Users { get; init; }
        public IEnumerable<ServerMember> Members { get; init; }
    }
}
