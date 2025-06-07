using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Hosting.Configuration
{
    internal sealed class RevoltConfiguration
    {
        public Uri ServerUrl { get; set; }
        public string Token { get; set; }
    }
}
