namespace Revolt.Net.Rest
{
    public class RestDirectMessageChannel : RestTextChannel
    {
        public bool Active { get; init; }

        public string[] Recipients { get; init; } = default!;
    }
}