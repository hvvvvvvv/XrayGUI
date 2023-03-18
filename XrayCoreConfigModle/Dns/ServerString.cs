using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.JsonConverters;

namespace XrayCoreConfigModle.Dns
{
    [JsonConverter(typeof(ServerStringConverter))]
    public class ServerString: DnsServer
    {
        string Value { get; set; }
        public ServerString(string value)
        {
            Value = value;
        }
        public void JsonWriteHandle(Utf8JsonWriter writer,JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, Value, options);
        }
        public static implicit operator ServerString(string value)
        {
            return new ServerString(value);
        }
    }
}
