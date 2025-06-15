using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Core.Common
{
    public static class RevoltFormatter
    {
        public static string Channel(string channelId) => $"<#{channelId}>";
    }
}
