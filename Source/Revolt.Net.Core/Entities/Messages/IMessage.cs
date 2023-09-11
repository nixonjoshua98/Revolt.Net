namespace Revolt.Net
{
    public interface IMessage
    {
        IMessageChannel Channel { get; }
        string Content { get; }
        string Id { get; }
        IUser Author { get; }

        Task AcknowledgeAsync();
        Task DeleteAsync();
        Task RemoveReactionsAsync();
        Task<IClientMessage> ReplyAsync(string content, Embed embed = null, IEnumerable<Embed> embeds = null, bool mention = false);
    }
}