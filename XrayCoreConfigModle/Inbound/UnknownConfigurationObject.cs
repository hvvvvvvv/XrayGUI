using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.JsonConverters;

namespace XrayCoreConfigModle.Inbound
{
    [JsonConverter(typeof(InboundUnknownConfigurationConverter))]
    public class UnknownConfigurationObject:InboundConfigurationObject
    {
        private JsonObject? Content;
        internal UnknownConfigurationObject(JsonObject? content = null)
        {
            Content = content;
        }
        public InboundConfigurationObject? ConverToSpecificType<T>() where T: InboundConfigurationObject
        {          
            if(typeof(T) == typeof(UnknownConfigurationObject))
            {
                return this;
            }
            return Content?.Deserialize<T>();
        }
        public void JsonWriterHandle(Utf8JsonWriter writer,JsonSerializerOptions options)
        {
            if (Content != null)
            {
                JsonSerializer.Serialize(writer, Content,options);
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }
        
    }
}
