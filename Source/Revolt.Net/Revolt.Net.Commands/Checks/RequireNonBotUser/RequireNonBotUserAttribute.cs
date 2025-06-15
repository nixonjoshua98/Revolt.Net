namespace Revolt.Net.Commands.Checks.RequireNonBotUser
{
    /// <summary>
    /// A command pre-check attribute that ensures the command is only executed by a non-bot user.
    /// </summary>
    /// <remarks>
    /// Apply this attribute to a command method to prevent bot accounts from triggering the command.
    /// Useful for avoiding command loops or unintended automation.
    /// </remarks>
    public sealed class RequireNonBotUserAttribute : CommandPreCheckAttribute;
}
