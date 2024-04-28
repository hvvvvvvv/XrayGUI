using log4net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class UDPOverTCPOConverter : JsonConverter<UDPOverTCPOptions>
    {
        public override UDPOverTCPOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;

            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
            {
                return new()
                {
                    Enabled = reader.GetBoolean(),
                    Version = 0
                };
            }
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var jsonDocument = JsonDocument.ParseValue(ref reader);
                var root = jsonDocument.RootElement;
                var udpOverTCPOptions = new UDPOverTCPOptions();
                var optionCopy = new JsonSerializerOptions(options);
                foreach (var p in typeof(UDPOverTCPOptions).GetProperties())
                {
                    if(Utils.PropertyIsIgnored(p.PropertyType, p.Name)) continue;
                    optionCopy.Converters.Clear();
                    var converter = Utils.GetProertyJsonConverter(p.PropertyType, p.Name);
                    if(converter != null) optionCopy.Converters.Add(converter);
                    var jsonName = Utils.GetPropertyJsonName(udpOverTCPOptions.GetType(), p.Name);
                    if (root.TryGetProperty(jsonName, out var jsonElement))
                    {
                        p.SetValue(udpOverTCPOptions, JsonSerializer.Deserialize(jsonElement, p.PropertyType, optionCopy));
                    }
                }
                return udpOverTCPOptions;
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, UDPOverTCPOptions value, JsonSerializerOptions options)
        {
            if(value.Enabled != null && value.Version == 0)
            {
                writer.WriteBoolean(Utils.GetPropertyJsonName(value.GetType(), nameof(value.Enabled)), value.Enabled.Value);
            }
            else
            {
                foreach(var p in typeof(UDPOverTCPOptions).GetProperties())
                {
                    var pvalue = p.GetValue(value);
                    Utils.WriteProertyToJson(writer, pvalue, typeof(UDPOverTCPOptions), p.Name, options);
                }
            }
        }
    }
}
