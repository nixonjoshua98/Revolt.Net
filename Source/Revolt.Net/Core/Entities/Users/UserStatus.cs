namespace Revolt.Net
{
    public sealed class UserStatus
    {
        public string Text { get; internal set; } = default!;

        public Presence Presence { get; internal set; } = default!;
    }
}
