using Revolt.Net.Commands.Info;
using Revolt.Net.Commands.Results;
using Revolt.Net.Rest;

namespace Revolt.Net.Commands.Attributes.Preconditions
{
    public class RequireGroupOwnerAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            return context.Channel is RestGroupChannel channel && channel.IsOwner(context.User) ?
                Task.FromResult(PreconditionResult.FromSuccess()) :
                Task.FromResult(PreconditionResult.FromError("This command can only be ran by the owner of this group."));
        }
    }
}