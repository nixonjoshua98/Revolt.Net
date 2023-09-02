using Revolt.Net.Client;
using Revolt.Net.Commands;
using Revolt.Net.Commands.Context;
using Revolt.Net.Websocket.Events.Incoming;

namespace Revolt.Net.TestBot
{
    internal static class CommandHandler
    {
        public static async Task SetupAsync(RevoltClient client, CommandService service, IServiceProvider provider)
        {
            await service.AddModuleAsync<HelloWorldModule>(provider);

            client.Message.Add(async e =>
            {
                if (ShouldHandle(e))
                {
                    var ctx = new CommandContext(e.Message);

                    var result = await service.ExecuteAsync(ctx, 1, provider, MultiMatchHandling.Best);
                }
            });
        }

        private static bool ShouldHandle(MessageEvent e)
        {
            return e.Message.Content.StartsWith("!") && !e.Message.Author.IsBot;
        }
    }
}
