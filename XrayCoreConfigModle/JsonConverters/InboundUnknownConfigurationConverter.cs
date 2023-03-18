using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;

namespace XrayCoreConfigModle.JsonConverters
{
    internal class InboundUnknownConfigurationConverter : JsonConverter<UnknownConfigurationObject>
    {
        public override UnknownConfigurationObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new UnknownConfigurationObject(JsonSerializer.Deserialize<JsonObject>(ref reader, options));
        }

        public override void Write(Utf8JsonWriter writer, UnknownConfigurationObject value, JsonSerializerOptions options)
        {
            value.JsonWriterHandle(writer, options);
        }
    }
}
