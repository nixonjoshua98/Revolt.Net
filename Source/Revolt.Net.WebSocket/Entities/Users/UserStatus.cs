namespace Revolt.Net.WebSocket
{
    public sealed class UserStatus
    {
        public string Text { get; internal set; } = default!;

        public Presence Presence { get; internal set; } = default!;

        internal void UpdateFromPartial(PartialUserStatus status)
        {
            status.Text.WhenHasValue(val => Text = val);
            status.Presence.WhenHasValue(val => Presence = val);
        }
    }
}
