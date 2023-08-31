namespace Revolt.Net.Core.Common.Types
{
    public readonly struct Optional<T>
    {
        public readonly T Value = default!;

        public readonly bool HasValue;

        public Optional()
        {
            HasValue = false;
        }

        public Optional(T value)
        {
            Value = value;
            HasValue = true;
        }

        public void WhenHasValue(Action<T> action)
        {
            if (HasValue)
            {
                action(Value);
            }
        }

        public static implicit operator Optional<T>(T value) => new(value);
    }
}
