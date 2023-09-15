namespace Revolt.Net
{
    public interface IMessageChannel : IChannel
    {
        Task<IMessage> GetMessageAsync(string messageId);
        Task<IClientMessage> SendMessageAsync(string content = null, Embed embed = null, IEnumerable<Embed> embeds = null);
    }
}
