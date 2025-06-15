namespace Revolt.Net.Core.JsonModels.Messages
{
    internal sealed record JsonMessageReply(
        string Id,
        bool Mention,
        bool FailIfNotExists
    );
}
