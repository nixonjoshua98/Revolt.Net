namespace Revolt.Net.Commands.Checks.RequireServer
{
    /// <summary>
    /// A command pre-check attribute that restricts command execution to a specific server (guild).
    /// </summary>
    /// <param name="serverId">The ID of the server where the command is allowed to run.</param>
    /// <remarks>
    /// Apply this attribute to a command method to ensure it only executes when invoked within the specified server.
    /// This is useful for commands that should only be available in a particular community or environment.
    /// </remarks>
    public sealed class RequireServerAttribute(string serverId) : CommandPreCheckAttribute
    {
        /// <summary>
        /// Gets the required server ID for the command to be executed.
        /// </summary>
        public readonly string ServerId = serverId;
    }

}
