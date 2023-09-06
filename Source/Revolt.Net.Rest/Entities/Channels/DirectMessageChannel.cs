namespace Revolt.Net
{
    public class DirectMessageChannel : MessageChannel
    {
        public bool Active { get; init; }

        public string[] Recipients { get; init; } = default!;
    }
}