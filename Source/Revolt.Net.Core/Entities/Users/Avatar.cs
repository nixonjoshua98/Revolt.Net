using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class Avatar
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        public string Tag { get; init; }

        [JsonPropertyName("filename")]
        public string FileName { get; init; }

        public Metadata Metadata { get; init; }

        public string ContentType { get; init; }

        public int Size { get; init; }

        public bool Deleted { get; init; }

        public bool Reported { get; init; }

        public string MessageId { get; init; }

        public string UserId { get; init; }

        public string ServerId { get; init; }

        public string ObjectId { get; init; }
    }

}
