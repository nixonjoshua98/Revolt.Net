﻿using Revolt.Net.Commands.Attributes;
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

        [Command("echo")]
        public async Task Echo(string message, int delay)
        {            
            var sentMessage = await Context.Channel.SendMessageAsync(message);

            await Task.Delay(delay);

            await sentMessage.DeleteAsync();
        }
    }
}
