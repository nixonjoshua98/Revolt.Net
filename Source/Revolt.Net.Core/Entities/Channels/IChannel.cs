namespace Revolt.Net
{
    public interface IChannel
    {
        ChannelType ChannelType { get; init; }
        string Id { get; init; }
    }
}