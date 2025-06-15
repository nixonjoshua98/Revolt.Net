using Revolt.Net.Commands;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Attributes;
using Revolt.Net.Commands.Checks.RequireNonBotUser;
using Revolt.Net.Commands.Checks.RequireServer;
using Revolt.Net.WebSocket.Entities;

namespace Revolt.Net.Samples.BasicCommands
{
    internal sealed class BasicComandModule : RevoltCommandModule
    {
        [Command("add")]
        [RequireNonBotUser]
        [RequireServer("01H93JVKBJBR3JKNWFTQN9ZWZ8")]
        public async Task Add(CommandContext context, int value1, int value2)
        {
            await context.Message.ReplyAsync($"{value1} + {value2} = {value1 + value2}");
        }

        [Command("size")]
        public async Task Size(CommandContext context, CancellationToken cancellationToken)
        {
            if (context.Message is ServerMessage msg)
            {
                var members = await msg.Server.GetMembersAsync(cancellationToken);

                await context.Message.ReplyAsync($"Members: {members.Count}", cancellationToken: cancellationToken);
            }
            else
            {
                await context.Message.ReplyAsync("Members: N/A", cancellationToken: cancellationToken);
            }
        }
    }
}