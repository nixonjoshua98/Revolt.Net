using Revolt.Commands;
using Revolt.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.TestBot
{
    public sealed class HelloWorldModule : ModuleBase
    {
        [Command("hello")]
        public async Task HelloAsync()
        {
            Console.WriteLine("World");
        }
    }
}
