namespace Revolt.Net.Core.Abstractions
{
    internal interface IMessage
    {
        string Id { get; }
        string AuthorId { get; }
        string ChannelId { get; }
        string? Content { get; }
    }
}
