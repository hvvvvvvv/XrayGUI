using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    [JsonConverter(typeof(JsonConverters.OutboundUnknownConfigurationObjectConverter))]
    public class UnknownConfigurationObject: OutboundConfigurationObject
    {
        private JsonObject? Content { get; set; }
        internal UnknownConfigurationObject(JsonObject? _content = null)
        {
            Content = _content;
        }
        public OutboundConfigurationObject? ConvertToSpecificType<T>() where T : OutboundConfigurationObject
        {
            if(typeof(T) == typeof(UnknownConfigurationObject))
            {
                return this;
            }
            return Content.Deserialize<T>();
        }
        public void JsonWriteHandle(Utf8JsonWriter writer,JsonSerializerOptions options)
        {
            if(Content != null)
            {
                JsonSerializer.Serialize(writer, Content, options);
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }

    }
}
