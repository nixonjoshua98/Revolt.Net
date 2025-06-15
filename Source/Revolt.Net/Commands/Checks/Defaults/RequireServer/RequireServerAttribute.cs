namespace Revolt.Net.Commands.Checks.Defaults.RequireServer
{
    public sealed class RequireServerAttribute(string serverId) : CommandPreCheckAttribute
    {
        public readonly string ServerId = serverId;
    }
}
