using Revolt.Net.Commands.Attributes;
using Revolt.Net.Commands.Module;

namespace Revolt.Net.TestBot
{
    public sealed class HelloWorldModule : CommandModuleBase
    {
        [Command("echo")]
        public async Task Echo(string message)
        {
            await Context.Message.ReplyAsync(content: message, embed: new Embed()
            {
                Title = "Echo",
                Description = message,
                Colour = RevoltColour.Green
            });
        }

        [Command("delete")]
        public async Task Delete()
        {
            await Context.Message.DeleteAsync();
        }
    }
}
