using System.Text.Json.Serialization;

namespace Revolt.Net.Entities.Media
{
    public class Attachment
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("filename")]
        public string Filename { get; init; } = default!;

        public AttachmentMetadata Metadata { get; init; } = default!;

        [JsonPropertyName("content_type")]
        public string ContentType { get; init; } = default!;

        public ulong Size { get; init; } = default!;

        public string Tag { get; init; } = default!;
    }
}
