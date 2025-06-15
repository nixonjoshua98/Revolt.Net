namespace Revolt.Net.Commands.Checks
{
    public interface ICommandPreCheck;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class CommandPreCheckAttribute : Attribute, ICommandPreCheck;

    public interface ICommandPreCheckHandler<TCheck> where TCheck : ICommandPreCheck
    {
        Task<CommandPreCheckResult> CheckAsync(CommandContext context, TCheck check, CancellationToken cancellationToken);
    }
}
