using Revolt.Net.Commands;
using Revolt.Net.WebSocket;

namespace Revolt.Net.TestBot
{
    internal static class CommandHandler
    {
        public static async Task SetupAsync(RevoltSocketClient client, CommandService service, IServiceProvider provider)
        {
            await service.AddModuleAsync<HelloWorldModule>(provider);

            client.Message += async e =>
            {
                if (ShouldHandle(e))
                {
                    var ctx = new CommandContext(e.Message);

                    var result = await service.ExecuteAsync(ctx, 1, provider, MultiMatchHandling.Best);
                }
            };
        }

        private static bool ShouldHandle(MessageEvent e)
        {
            return e.Message.Content.StartsWith("!") && !e.Message.Author.IsBot;
        }
    }
}
