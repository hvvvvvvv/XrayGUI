using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class V2RayTransportConverter : JsonConverter<V2RayTransportOptions>
    {
        public override V2RayTransportOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;
            var transport = new V2RayTransportOptions();
            var obj = JsonNode.Parse(ref reader)?.AsObject() ?? throw new JsonException();
            var jsonName = Utils.GetPropertyJsonName(transport.GetType(), nameof(transport.Type));
            transport.Type = obj[jsonName]?.GetValue<string>() ?? throw new JsonException();
            obj.Remove(jsonName);
            switch(transport.Type)
            {
                case V2RayTransportTypes.HTTP:
                    transport.HTTPOptions = obj.Deserialize<V2RayHTTPOptions>(options);
                    break;
                case V2RayTransportTypes.WebSocket:
                    transport.WebsocketOptions = obj.Deserialize<V2RayWebsocketOptions>(options);
                    break;
                case V2RayTransportTypes.GRPC:
                    transport.GRPCOptions = obj.Deserialize<V2RayGRPCOptions>(options);
                    break;
                case V2RayTransportTypes.HTTPUpgrade:
                    transport.HTTPUpgradeOptions = obj.Deserialize<V2RayHTTPUpgradeOptions>(options);
                    break;
                case V2RayTransportTypes.QUIC:
                    transport.QUICOptions = obj.Deserialize<V2RayQUICOptions>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return transport;
        }

        public override void Write(Utf8JsonWriter writer, V2RayTransportOptions value, JsonSerializerOptions options)
        {
            (var transportType, var transportValue) = value.Type switch
            {
                V2RayTransportTypes.HTTP => (typeof(V2RayHTTPOptions), value.HTTPOptions as object),
                V2RayTransportTypes.WebSocket => (typeof(V2RayWebsocketOptions), value.WebsocketOptions),
                V2RayTransportTypes.GRPC => (typeof(V2RayGRPCOptions), value.GRPCOptions),
                V2RayTransportTypes.HTTPUpgrade => (typeof(V2RayHTTPUpgradeOptions), value.HTTPUpgradeOptions),
                V2RayTransportTypes.QUIC => (typeof(V2RayQUICOptions), value.QUICOptions),
                _ => throw new JsonException(),
            };
            writer.WriteStartObject();
            writer.WritePropertyName(Utils.GetPropertyJsonName(typeof(V2RayTransportOptions), nameof(value.Type)));
            JsonSerializer.Serialize(writer, value.Type, options);
            foreach(var p in transportType.GetProperties())
            {
                var pvalue = p.GetValue(transportValue);
                if (pvalue == null && options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull)
                {
                    continue;
                }
                writer.WritePropertyName(Utils.GetPropertyJsonName(transportType, p.Name));
                JsonSerializer.Serialize(writer, pvalue, options);
            }
            writer.WriteEndObject();
        }
    }
}
