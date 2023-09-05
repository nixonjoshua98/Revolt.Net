namespace Revolt.Net.WebSocket
{

    internal sealed class UserUpdateMessage
    {
        public string Id { get; init; } = default!;
        public PartialUser Data { get; init; } = default!;

        internal UserUpdateEvent ToEvent() => new(Id);
    }

    public sealed record UserUpdateEvent(string UserId);
}