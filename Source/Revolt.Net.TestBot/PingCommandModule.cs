using Revolt.Net.Commands;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Attributes;
using Revolt.Net.Rest.Extensions;

namespace Revolt.Net.TestBot
{
    internal sealed class PingCommandModule : CommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext context)
        {
            var msg = await context.Message.ReplyAsync("Replied");

            msg = await msg.EditAsync("Edited");

            var channel = await msg.GetChannelAsync();

            await msg.ReplyAsync(channel.Id);

            msg = await channel.GetMessageAsync(msg.Id);

            await msg.EditAsync("Fetched and edited");

            await msg.RefreshAsync();
        }
    }
}