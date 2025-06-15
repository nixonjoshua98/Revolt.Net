namespace Revolt.Net.Commands.Checks
{
    public readonly record struct CommandPreCheckResult(bool IsSuccess, string? Message = null)
    {
        public static CommandPreCheckResult Success() => new(true);

        public static CommandPreCheckResult Failed(string? message = null) => new(false, message);
    }
}
