namespace Revolt.Net.Core
{
    public sealed record RevoltConfiguration(string ApiUrl)
    {
        public static readonly RevoltConfiguration Default = new("https://api.revolt.chat/");
    }
}
