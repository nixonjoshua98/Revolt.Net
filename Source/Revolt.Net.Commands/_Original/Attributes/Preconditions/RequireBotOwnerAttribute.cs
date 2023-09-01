using Revolt.Net.Commands._Original.Info;
using Revolt.Net.Commands._Original.Results;
using Revolt.Net.Commands.Context;

namespace Revolt.Net.Commands._Original.Attributes.Preconditions
{
    public sealed class RequireBotOwnerAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(
            ICommandContext context,
            CommandInfo command,
            IServiceProvider services)
        {
            return context.Client.IsOwner(context.User.Id) ?
                Task.FromResult(PreconditionResult.FromSuccess()) :
                Task.FromResult(PreconditionResult.FromError("This command can only be executed by the owner of this bot."));
        }
    }
}