namespace Revolt.Net.Commands.Checks.RequireChannel
{
    /// <summary>
    /// A command pre-check attribute that restricts command execution to a specific channel.
    /// </summary>
    /// <param name="channelId">The ID of the channel where the command is allowed to run.</param>
    /// <remarks>
    /// Apply this attribute to a command method to ensure it only executes when invoked in the specified channel.
    /// </remarks>
    public sealed class RequireChannelAttribute(string channelId) : CommandPreCheckAttribute
    {
        public readonly string ChannelId = channelId;
    }

}
