using System.Text.Json;
using System.Text.Json.Serialization;

namespace Revolt.Net.Json
{
    internal sealed class RevoltColorConverter : JsonConverter<RevoltColour>
    {
        public override RevoltColour Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            var ele = doc.RootElement;

            var str = ele.GetRawText();

            return reader.TokenType == JsonTokenType.Null ?
                null : RevoltColour.Create(str);
        }

        public override void Write(Utf8JsonWriter writer, RevoltColour value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
