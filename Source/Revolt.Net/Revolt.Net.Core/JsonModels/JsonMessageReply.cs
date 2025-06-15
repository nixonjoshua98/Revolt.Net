namespace Revolt.Net.Core.JsonModels
{
    internal sealed record JsonMessageReply(
        string Id,
        bool Mention,
        bool FailIfNotExists
    );
}
