namespace Revolt.Net
{
    internal sealed record MessageReply(string Id, bool Mention)
    {
        public static bool TryCreate(string id, bool Mention, out MessageReply result)
        {
            result = default!;

            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            result = new MessageReply(id, Mention);

            return true;
        }
    }
}
