using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.OutBound;

namespace XrayCoreConfigModle.JsonConverters
{
    internal class OutboundConfigurationObjectConverter : JsonConverter<OutboundConfigurationObject>
    {
        public override OutboundConfigurationObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<UnknownConfigurationObject>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, OutboundConfigurationObject value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
