using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Core.JsonModels.Messages
{
    internal sealed record JsonMessageReply(
        string Id, 
        bool Mention, 
        bool FailIfNotExists
    );
}
