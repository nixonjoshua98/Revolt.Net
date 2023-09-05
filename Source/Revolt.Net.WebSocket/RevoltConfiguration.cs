namespace Revolt.Net.WebSocket
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
