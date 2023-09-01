using Revolt.Net.Commands.Attributes;
using Revolt.Net.Commands.Module;

namespace Revolt.Net.TestBot
{
    public sealed class HelloWorldModule : CommandModuleBase
    {
        [Command("echo")]
        public async Task Echo(string message)
        {
            await Context.Message.ReplyAsync(message);
        }

        [Command("delete")]
        public async Task Delete()
        {
            await Context.Message.DeleteAsync();
        }
    }
}
