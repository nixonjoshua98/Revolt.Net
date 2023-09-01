using Revolt.Net.Commands._Original.Info;
using Revolt.Net.Commands._Original.Results;
using Revolt.Net.Commands.Context;
using Revolt.Net.Entities.Channels;

namespace Revolt.Net.Commands._Original.Attributes.Preconditions
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