using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Dns;
using XrayCoreConfigModle.OutBound;

namespace XrayCoreConfigModle.JsonConverters
{
    internal class ServerStringConverter : JsonConverter<ServerString>
    {
        public override ServerString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString() ?? string.Empty;
            }
            else if(reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, ServerString value, JsonSerializerOptions options)
        {
            value.JsonWriteHandle(writer, options);
        }
    }
}
