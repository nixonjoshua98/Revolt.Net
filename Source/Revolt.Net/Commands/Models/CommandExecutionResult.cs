namespace Revolt.Net.Commands.Models
{
    public enum CommandExecutionError { Exception, CommandNotFound, BindingFailed };

    public sealed record CommandExecutionResult(CommandExecutionError? Error, string? ErrorMessage)
    {
        public static CommandExecutionResult AsSuccess() =>
            new CommandExecutionResult(null, null);

        public static CommandExecutionResult AsError(CommandExecutionError error, string errorMessage) =>
            new CommandExecutionResult(error, errorMessage);
    }
}
