namespace Revolt.Net.Core.Entities.Channels
{
    public class MessageChannel : Channel
    {
        public string[] Recipients { get; init; } = default!;
    }
}