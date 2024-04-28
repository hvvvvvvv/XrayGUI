using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class ListaleConverter<T> : JsonConverter<Listable<T>>
    {
        public override Listable<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            var jsonNode = JsonNode.Parse(ref reader);
            if(jsonNode is JsonArray jsonArray)
            {
                return (jsonArray.Deserialize<List<T>>(options) ?? throw new JsonException());
            }
            else
            {
                return new List<T> { jsonNode.Deserialize<T>(options) ?? throw new JsonException()};
            }
        }

        public override void Write(Utf8JsonWriter writer, Listable<T> value, JsonSerializerOptions options)
        {
            if(value.Count == 1)
            {
                JsonSerializer.Serialize(writer, value[0], typeof(T), options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, typeof(IList<T>), options);
            }
        }
       
    }
}
