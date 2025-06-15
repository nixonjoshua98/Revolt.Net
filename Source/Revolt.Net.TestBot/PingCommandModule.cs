using Revolt.Net.Commands;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Attributes;
using Revolt.Net.Commands.Checks.Defaults.RequireChannel;
using Revolt.Net.Rest.Extensions;

namespace Revolt.Net.TestBot
{
    internal sealed class PingCommandModule : CommandModule
    {
        [RequireChannel("01JX5997APK46VPGMN54T9AJ12")]
        [Command("ping")]
        public async Task Ping(CommandContext context)
        {
            await context.Message.ReplyAsync("Pong");
        }
    }
}