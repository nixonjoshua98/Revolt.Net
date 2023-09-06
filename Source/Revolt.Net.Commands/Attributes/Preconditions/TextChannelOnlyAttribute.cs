using Revolt.Net.Commands.Context;
using Revolt.Net.Commands.Info;
using Revolt.Net.Commands.Results;

namespace Revolt.Net.Commands.Attributes.Preconditions
{
    public class TextChannelOnlyAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            return context.Channel is MessageChannel ?
                Task.FromResult(PreconditionResult.FromSuccess()) :
                Task.FromResult(PreconditionResult.FromError("This command can only be executed in a text channel."));
        }
    }
}