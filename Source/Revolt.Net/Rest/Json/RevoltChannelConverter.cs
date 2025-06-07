using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest.Json
{
    internal class RevoltChannelConverter : JsonConverter<RestChannel>
    {
        public override RestChannel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            var ele = doc.RootElement;

            var json = ele.GetRawText();
            var node = JsonNode.Parse(json);

            var channelType = Enum.Parse<ChannelType>(node!["channel_type"]!.ToString());

            return channelType switch
            {
                ChannelType.TextChannel => node.Deserialize<RestTextChannel>(options),
                ChannelType.Group => node.Deserialize<RestGroupChannel>(options),
                ChannelType.DirectMessage => node.Deserialize<RestDirectMessageChannel>(options),
                ChannelType.SavedMessages => node.Deserialize<RestSavedMessagesChannel>(options),
                _ => throw new Exception()
            }; ;
        }

        public override void Write(Utf8JsonWriter writer, RestChannel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
