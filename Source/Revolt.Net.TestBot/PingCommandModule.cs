using Revolt.Net.Commands;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Attributes;
using Revolt.Net.Rest.Extensions;

namespace Revolt.Net.TestBot
{
    enum TestEnum { Hello, Goodbye }

    internal sealed class PingCommandModule : CommandModule
    {
        [Command("ping", priority: 1)]
        public async Task Ping1(CommandContext context, TestEnum value)
        {
            await context.Message.ReplyAsync($"Ping: {value}:{(int)value}");
        }

        [Command("ping", priority: 2)]
        public async Task Ping2(CommandContext context, int value1, int value2)
        {
            await context.Message.ReplyAsync($"Ping: {value1}:{value2}");
        }
    }
}