namespace Revolt.Net.Core
{
    public class RevoltConfiguration
    {
        public string ApiUrl { get; init; } = "https://api.revolt.chat/";
    }

    public class RevoltBotConfiguration : RevoltConfiguration
    {
        public string Token { get; set; } = default!;
    }
}
