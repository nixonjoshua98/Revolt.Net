namespace Revolt.Net
{
    internal readonly struct Union<T1, T2>
    {
        public readonly T1 Left = default!;
        public readonly T2 Right = default!;

        public readonly bool IsLeft;

        public Union(T1 left)
        {
            Left = left;
            IsLeft = true;
        }

        public Union(T2 right)
        {
            Right = right;
            IsLeft = false;
        }

        public static implicit operator Union<T1, T2>(T1 left) => new(left);
        public static implicit operator Union<T1, T2>(T2 right) => new(right);
    }
}
