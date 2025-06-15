namespace Revolt.Net.Commands.Checks
{
    /// <summary>
    /// Represents the result of a command pre-check, indicating whether the check passed or failed,
    /// along with an optional message explaining the failure.
    /// </summary>
    /// <param name="IsSuccess">Indicates whether the pre-check succeeded.</param>
    /// <param name="Message">An optional message providing context or explanation when the check fails.</param>
    public readonly record struct CommandPreCheckResult(bool IsSuccess, string? Message = null)
    {
        /// <summary>
        /// Creates a successful pre-check result.
        /// </summary>
        public static CommandPreCheckResult Success() => new(true);

        /// <summary>
        /// Creates a failed pre-check result with an optional failure message.
        /// </summary>
        /// <param name="message">A message describing why the check failed (optional).</param>
        public static CommandPreCheckResult Failed(string? message = null) => new(false, message);
    }

}
