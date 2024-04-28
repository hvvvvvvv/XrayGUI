using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Outbound;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class OutboundConverter : JsonConverter<OutboundOptions>
    {
        public override OutboundOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;
            var outboundOptions = new OutboundOptions();
            var jsonObj = JsonNode.Parse(ref reader) as JsonObject ?? throw new JsonException();
            var jsonName = Utils.GetPropertyJsonName(outboundOptions.GetType(), nameof(outboundOptions.Type));
            outboundOptions.Type = jsonObj[jsonName]?.GetValue<string>() ?? throw new JsonException();
            jsonObj.Remove(jsonName);
            jsonName = Utils.GetPropertyJsonName(outboundOptions.GetType(), nameof(outboundOptions.Tag));
            outboundOptions.Tag = jsonObj[jsonName]?.GetValue<string?>();
            jsonObj.Remove(jsonName);
            switch (outboundOptions.Type)
            {
                case OutboundTypes.Block:
                case OutboundTypes.DNS:
                    break;
                case OutboundTypes.Direct:
                    outboundOptions.DirectOptions = jsonObj.Deserialize<DirectOutboundOptions>(options);
                    break;
                case OutboundTypes.Socks:
                    outboundOptions.SocksOptions = jsonObj.Deserialize<SocksOutboundOptions>(options);
                    break;
                case OutboundTypes.HTTP:
                    outboundOptions.HTTPOptions = jsonObj.Deserialize<HTTPOutboundOptions>(options);
                    break;
                case OutboundTypes.Shadowsocks:
                    outboundOptions.ShadowsocksOptions = jsonObj.Deserialize<ShadowsocksOutboundOptions>(options);
                    break;
                case OutboundTypes.VMss:
                    outboundOptions.VMessOptions = jsonObj.Deserialize<VMessOutboundOptions>(options);
                    break;
                case OutboundTypes.Trojan:
                    outboundOptions.TrojanOptions = jsonObj.Deserialize<TrojanOutboundOptions>(options);
                    break;
                case OutboundTypes.WireGuard:
                    outboundOptions.WireGuardOptions = jsonObj.Deserialize<WireGuardOutboundOptions>(options);
                    break;
                case OutboundTypes.Hysteria:
                    outboundOptions.HysteriaOptions = jsonObj.Deserialize<HysteriaOutboundOptions>(options);
                    break;
                case OutboundTypes.Tor:
                    outboundOptions.TorOptions = jsonObj.Deserialize<TorOutboundOptions>(options);
                    break;
                case OutboundTypes.SSH:
                    outboundOptions.SSHOptions = jsonObj.Deserialize<SSHOutboundOptions>(options);
                    break;
                case OutboundTypes.ShadowTLS:
                    outboundOptions.ShadowTLSOptions = jsonObj.Deserialize<ShadowTLSOutboundOptions>(options);
                    break;
                case OutboundTypes.ShadowsocksR:
                    outboundOptions.ShadowsocksROptions = jsonObj.Deserialize<ShadowsocksROutboundOptions>(options);
                    break;
                case OutboundTypes.VLESS:
                    outboundOptions.VLESSOptions = jsonObj.Deserialize<VLESSOutboundOptions>(options);
                    break;
                case OutboundTypes.TUIC:
                    outboundOptions.TUICOptions = jsonObj.Deserialize<TUICOutboundOptions>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return outboundOptions;
        }

        public override void Write(Utf8JsonWriter writer, OutboundOptions value, JsonSerializerOptions options)
        {
            (var optionType, var optionValue) = value.Type switch
            {               
                OutboundTypes.Direct => (typeof(DirectOutboundOptions), value.DirectOptions as object),
                OutboundTypes.Socks => (typeof(SocksOutboundOptions), value.SocksOptions),
                OutboundTypes.HTTP => (typeof(HTTPOutboundOptions), value.HTTPOptions),
                OutboundTypes.Shadowsocks => (typeof(ShadowsocksOutboundOptions), value.ShadowsocksOptions),
                OutboundTypes.VMss => (typeof(VMessOutboundOptions), value.VMessOptions),
                OutboundTypes.Trojan => (typeof(TrojanOutboundOptions), value.TrojanOptions),
                OutboundTypes.WireGuard => (typeof(WireGuardOutboundOptions), value.WireGuardOptions),
                OutboundTypes.Hysteria => (typeof(HysteriaOutboundOptions), value.HysteriaOptions),
                OutboundTypes.Tor => (typeof(TorOutboundOptions), value.TorOptions),
                OutboundTypes.SSH => (typeof(SSHOutboundOptions), value.SSHOptions),
                OutboundTypes.ShadowTLS => (typeof(ShadowTLSOutboundOptions), value.ShadowTLSOptions),
                OutboundTypes.ShadowsocksR => (typeof(ShadowsocksROutboundOptions), value.ShadowsocksROptions),
                OutboundTypes.VLESS => (typeof(VLESSOutboundOptions), value.VLESSOptions),
                OutboundTypes.TUIC => (typeof(TUICOutboundOptions), value.TUICOptions),
                OutboundTypes.Block or OutboundTypes.DNS => (null, null),
                _ => throw new JsonException()
            };
            writer.WriteStartObject();
            foreach (var property in typeof(OutboundOptions).GetProperties())
            {
                var propertyValue = property.GetValue(value);
                Utils.WriteProertyToJson(writer, propertyValue, typeof(OutboundOptions), property.Name, options);
            }
            if(optionType == null)
            {
                writer.WriteEndObject();
                return;
            }
            foreach (var property in optionType.GetProperties())
            {
                var propertyValue = property.GetValue(optionValue);              
                Utils.WriteProertyToJson(writer, propertyValue, optionType, property.Name, options);
            }
            writer.WriteEndObject();
        }
    }
}
