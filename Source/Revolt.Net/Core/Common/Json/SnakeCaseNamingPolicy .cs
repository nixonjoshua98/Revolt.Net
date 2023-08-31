using System.Text.Json;

namespace Revolt.Net.Core.Common.Json
{
    internal sealed class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static readonly SnakeCaseNamingPolicy Instance = new();

        public override string ConvertName(string name)
        {
            return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
