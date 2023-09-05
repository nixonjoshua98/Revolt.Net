namespace Revolt.Net.WebSocket
{
    public class MessageChannel : Channel
    {
        public string[] Recipients { get; init; } = default!;

        public async Task<ClientMessage> SendMessageAsync(string content)
        {
            return await Client.State.Messages.SendAsync(Id, content);
        }

        public async Task<Message> GetMessageAsync(string messageId)
        {
            return await Client.State.Messages.GetAsync(Id, messageId);
        }
    }
}