namespace Revolt.Net
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

        public void Match(Action<T> action)
        {
            if (HasValue)
            {
                action(Value);
            }
        }

        public TValue Match<TValue>(Func<T, TValue> factory, TValue fallback)
        {
            return HasValue ? factory(Value) : fallback;
        }

        public static implicit operator Optional<T>(T value) => new(value);
    }
}
