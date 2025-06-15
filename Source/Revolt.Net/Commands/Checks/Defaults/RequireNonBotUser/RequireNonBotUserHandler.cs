using Revolt.Net.Rest.Extensions;

namespace Revolt.Net.Commands.Checks.Defaults.MustNotBeBot
{
    internal sealed class RequireNonBotUserHandler : ICommandPreCheckHandler<RequireNonBotUserAttribute>
    {
        public Task<CommandPreCheckResult> CheckAsync(CommandContext context, RequireNonBotUserAttribute check, CancellationToken cancellationToken)
        {
            var result = context.Message.Author.IsBot ? 
                CommandPreCheckResult.Failed() : 
                CommandPreCheckResult.Success();

            return Task.FromResult(result);
        }
    }
}
