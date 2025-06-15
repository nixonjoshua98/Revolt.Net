namespace Revolt.Net.Commands.Models
{
    public enum CommandExecutionError { Exception, CommandNotFound, BindingFailed, PreCheckFailed };

    public readonly record struct CommandExecutionResult(CommandExecutionError? Error, string? ErrorMessage)
    {
        public bool IsSuccess => !Error.HasValue;

        public static CommandExecutionResult Success() =>
            new CommandExecutionResult(null, null);

        public static CommandExecutionResult Failure(CommandExecutionError error, string errorMessage) =>
            new CommandExecutionResult(error, errorMessage);
    }
}
