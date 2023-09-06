namespace Revolt.Net.Rest
{
    public class RestDirectMessageChannel : RestMessageChannel
    {
        public bool Active { get; init; }

        public string[] Recipients { get; init; } = default!;
    }
}