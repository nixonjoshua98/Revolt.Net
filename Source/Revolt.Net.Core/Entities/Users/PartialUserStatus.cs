namespace Revolt.Net
{
    public sealed class PartialUserStatus
    {
        public Optional<string> Text { get; init; } = default!;

        public Optional<Presence> Presence { get; init; } = default!;
    }
}
