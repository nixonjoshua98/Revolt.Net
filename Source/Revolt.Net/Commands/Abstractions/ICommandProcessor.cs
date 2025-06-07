namespace Revolt.Net.Commands.Abstractions
{
    public interface ICommandProcessor
    {
        Task ExecuteAsync(CommandContext context, int startIndex, CancellationToken cancellationToken);
    }
}