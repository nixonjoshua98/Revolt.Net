using Revolt.Net.Commands.Info;
using Revolt.Net.Commands.Results;
using Revolt.Net.Rest;

namespace Revolt.Net.Commands.Attributes.Preconditions
{
    public class TextChannelOnlyAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            return context.Channel is RestTextChannel ?
                Task.FromResult(PreconditionResult.FromSuccess()) :
                Task.FromResult(PreconditionResult.FromError("This command can only be executed in a text channel."));
        }
    }
}