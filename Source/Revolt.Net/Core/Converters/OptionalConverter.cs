using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.Converters
{
    internal sealed class OptionalConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType) return false;

            return typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var wrappedType = typeToConvert.GetGenericArguments()[0];

            return Activator.CreateInstance(typeof(OptionalConverter<>).MakeGenericType(wrappedType)) as JsonConverter;
        }
    }

    internal sealed class OptionalConverter<T> : JsonConverter<Optional<T>>
    {
        public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            var ele = doc.RootElement;

            var json = ele.GetRawText();
            var node = JsonNode.Parse(json);

            return reader.TokenType == JsonTokenType.Null ?
                new Optional<T>() :
                node.Deserialize<T>(options)!;
        }

        public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
