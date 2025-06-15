namespace Revolt.Net.Commands.Checks.Defaults.RequireChannel
{
    public sealed class RequireChannelAttribute(string channelId) : CommandPreCheckAttribute
    {
        public readonly string ChannelId = channelId;
    }
}
