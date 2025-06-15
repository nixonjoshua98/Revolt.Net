using System.Text.RegularExpressions;

namespace Revolt.Net
{
    public partial class RevoltColour
    {
        public static readonly RevoltColour Green;

        static RevoltColour()
        {
            Green = Create("green");
        }
    }


    public partial class RevoltColour
    {
        public string Value { get; protected set; }

        private RevoltColour() { }

        private RevoltColour(string value)
        {
            Value = value;
        }

        [GeneratedRegex("(?i)^(?:[a-z ]+|var\\(--[a-z\\d-]+\\)|rgba?\\([\\d, ]+\\)|#[a-f0-9]+|(repeating-)?(linear|conic|radial)-gradient\\(([a-z ]+|var\\(--[a-z\\d-]+\\)|rgba?\\([\\d, ]+\\)|#[a-f0-9]+|\\d+deg)([ ]+(\\d{1,3}%|0))?(,[ ]*([a-z ]+|var\\(--[a-z\\d-]+\\)|rgba?\\([\\d, ]+\\)|#[a-f0-9]+)([ ]+(\\d{1,3}%|0))?)+\\))$", RegexOptions.Compiled, "en-GB")]
        public static partial Regex Regex();

        public static RevoltColour Create(string value)
        {
            return new RevoltColour(value);
        }

        public static bool TryCreate(string value, out RevoltColour color)
        {
            color = default!;

            if (!ValidateString(value))
            {
                return false;
            }

            color = new RevoltColour(value);

            return true;
        }

        private static bool ValidateString(string value) => Regex().IsMatch(value);
    }
}
