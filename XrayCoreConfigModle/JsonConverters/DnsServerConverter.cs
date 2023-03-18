using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Dns;

namespace XrayCoreConfigModle.JsonConverters
{
    internal class DnsServerConverter : JsonConverter<DnsServer>
    {
        public override DnsServer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                return JsonSerializer.Deserialize<ServerObject>(ref reader, options);
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                return stringValue != null ? new ServerString(stringValue) : null;
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, DnsServer value, JsonSerializerOptions options)
        {
            if(value is ServerObject serverObject)
            {
                JsonSerializer.Serialize(writer, serverObject, options);
            }
            else if(value is ServerString serverString)
            {
                serverString.JsonWriteHandle(writer, options);
            }
            else if(value is null)
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
            else
            {
                throw new JsonException();
            }
        }
    }
}
