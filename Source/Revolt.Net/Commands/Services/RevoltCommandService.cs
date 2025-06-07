using Revolt.Net.Commands.Abstractions;

namespace Revolt.Net.Commands.Services
{
    internal sealed class RevoltCommandService : ICommandProcessor
    {
        public async Task ExecuteAsync(CommandContext context, int startIndex, CancellationToken cancellationToken)
        {

        }
    }
}
