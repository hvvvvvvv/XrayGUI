using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.JsonConverters
{
    internal class FakeDNSObjectConverter : JsonConverter<FakeDNSObject>
    {
        public override FakeDNSObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var retVlaue = new FakeDNSObject();
                        
            if(reader.TokenType == JsonTokenType.StartObject)
            {
                var addValue = JsonSerializer.Deserialize<FakeDnsIpPoolObject>(ref reader, options);
                if (addValue != null)
                {
                    retVlaue.Add(addValue);
                }
            }
            else if(reader.TokenType == JsonTokenType.StartArray)
            {
                while(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    var addValue = JsonSerializer.Deserialize<FakeDnsIpPoolObject>(ref reader, options);
                    if (addValue != null)
                    {
                        retVlaue.Add(addValue);
                    }
                }
            }
            else if(reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            else
            {
                throw new JsonException();
            }
            return retVlaue;
        }

        public override void Write(Utf8JsonWriter writer, FakeDNSObject value, JsonSerializerOptions options)
        {
            if(value?.Count == 1)
            {

                JsonSerializer.Serialize(writer, value[0], options);

            }
            else if(value?.Count > 1)
            {
                writer.WriteStartArray();
                foreach(var item in value)
                {
                    JsonSerializer.Serialize(writer, item, options);
                }
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }
    }
}
