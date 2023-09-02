using Revolt.Net.Commands.Context;
using Revolt.Net.Commands.Info;
using Revolt.Net.Commands.Results;
using Revolt.Net.Entities.Channels;

namespace Revolt.Net.Commands.Attributes.Preconditions
{
    public sealed class GroupOnlyAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            return context.Channel is GroupChannel ?
                Task.FromResult(PreconditionResult.FromSuccess()) :
                Task.FromResult(PreconditionResult.FromError("This command can only be executed in a group channel."));
        }
    }
}