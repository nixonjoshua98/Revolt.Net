namespace Revolt.Net.Core.Common.Types
{
    public readonly struct Optional<T>
    {
        public readonly T Value = default!;

        public readonly bool IsSpecified;

        public Optional()
        {
            IsSpecified = false;
        }

        public Optional(T value)
        {
            Value = value;
            IsSpecified = true;
        }

        public void WhenHasValue(Action<T> action)
        {
            if (IsSpecified)
            {
                action(Value);
            }
        }

        public static implicit operator Optional<T>(T value) => new(value);
    }
}
