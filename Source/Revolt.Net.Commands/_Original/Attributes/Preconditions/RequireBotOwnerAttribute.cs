using Revolt.Commands.Info;
using Revolt.Commands.Results;

namespace Revolt.Commands.Attributes.Preconditions
{
    public class RequireBotOwnerAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            bool isOwner = context.Client.IsOwner(context.Message.AuthorId);

            return Task.FromResult(
                isOwner ?
                    PreconditionResult.FromSuccess() :
                    PreconditionResult.FromError("This command can only be executed by the owner of this bot.")
            );
        }
    }
}