using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Inbound;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class InboundConverter : JsonConverter<InboundOptions>
    {
        public override InboundOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;

            var inbounOptions = new InboundOptions();
            var jsonObj = JsonNode.Parse(ref reader) as JsonObject ?? throw new JsonException();
            var jsonName = Utils.GetPropertyJsonName(inbounOptions.GetType(), nameof(inbounOptions.Type));
            inbounOptions.Type = jsonObj[jsonName]?.GetValue<string>() ?? throw new JsonException();
            jsonObj.Remove(jsonName);
            jsonName = Utils.GetPropertyJsonName(inbounOptions.GetType(), nameof(inbounOptions.Tag));
            inbounOptions.Tag = jsonObj[jsonName]?.GetValue<string?>();
            jsonObj.Remove(jsonName);
            switch(inbounOptions.Type)
            {
                case InboundTypes.Tun:
                    inbounOptions.TunOptions = jsonObj.Deserialize<TunInboundOptions>(options);
                    break;
                case InboundTypes.Redirect:
                    inbounOptions.RedirectOptions = jsonObj.Deserialize<RedirectInboundOptions>(options);
                    break;
                case InboundTypes.TProxy:
                    inbounOptions.TProxyOptions = jsonObj.Deserialize<TProxyInboundOptions>(options);
                    break;
                case InboundTypes.Direct:
                    inbounOptions.DirectOptions = jsonObj.Deserialize<DirectInboundOptions>(options);
                    break;
                case InboundTypes.Socks:
                    inbounOptions.SocksOptions = jsonObj.Deserialize<SocksInboundOptions>(options);
                    break;
                case InboundTypes.HTTP:
                    inbounOptions.HTTPOptions = jsonObj.Deserialize<HTTPMixedInboundOptions>(options);
                    break;
                case InboundTypes.Mixed:
                    inbounOptions.MixedOptions = jsonObj.Deserialize<HTTPMixedInboundOptions>(options);
                    break;
                case InboundTypes.Shadowsocks:
                    inbounOptions.ShadowsocksOptions = jsonObj.Deserialize<ShadowsocksInboundOptions>(options);
                    break;
                case InboundTypes.VMess:
                    inbounOptions.VMessOptions = jsonObj.Deserialize<VMessInboundOptions>(options);
                    break;
                case InboundTypes.Trojan:
                    inbounOptions.TrojanOptions = jsonObj.Deserialize<TrojanInboundOptions>(options);
                    break;
                case InboundTypes.Naive:
                    inbounOptions.NaiveOptions = jsonObj.Deserialize<NaiveInboundOptions>(options);
                    break;
                case InboundTypes.Hysteria:
                    inbounOptions.HysteriaOptions = jsonObj.Deserialize<HysteriaInboundOptions>(options);
                    break;
                case InboundTypes.ShadowTLS:
                    inbounOptions.ShadowTLSOptions = jsonObj.Deserialize<ShadowTLSInboundOptions>(options);
                    break;
                case InboundTypes.VLESS:
                    inbounOptions.VLESSOptions = jsonObj.Deserialize<VLESSInboundOptions>(options);
                    break;
                case InboundTypes.TUIC:
                    inbounOptions.TUICOptions = jsonObj.Deserialize<TUICInboundOptions>(options);
                    break;
                case InboundTypes.Hysteria2:
                    inbounOptions.Hysteria2Options = jsonObj.Deserialize<Hysteria2InboundOptions>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return inbounOptions;
        }

        public override void Write(Utf8JsonWriter writer, InboundOptions value, JsonSerializerOptions options)
        {
            (var optionType, var OoptionValue) = value.Type switch
            {
                InboundTypes.Tun => (typeof(TunInboundOptions), value.TunOptions as object),
                InboundTypes.Redirect => (typeof(RedirectInboundOptions), value.RedirectOptions),
                InboundTypes.TProxy => (typeof(TProxyInboundOptions), value.TProxyOptions),
                InboundTypes.Direct => (typeof(DirectInboundOptions), value.DirectOptions),
                InboundTypes.Socks => (typeof(SocksInboundOptions), value.SocksOptions),
                InboundTypes.HTTP => (typeof(HTTPMixedInboundOptions), value.HTTPOptions),
                InboundTypes.Mixed => (typeof(HTTPMixedInboundOptions), value.MixedOptions),
                InboundTypes.Shadowsocks => (typeof(ShadowsocksInboundOptions), value.ShadowsocksOptions),
                InboundTypes.VMess => (typeof(VMessInboundOptions), value.VMessOptions),
                InboundTypes.Trojan => (typeof(TrojanInboundOptions), value.TrojanOptions),
                InboundTypes.Naive => (typeof(NaiveInboundOptions), value.NaiveOptions),
                InboundTypes.Hysteria => (typeof(HysteriaInboundOptions), value.HysteriaOptions),
                InboundTypes.ShadowTLS => (typeof(ShadowTLSInboundOptions), value.ShadowTLSOptions),
                InboundTypes.VLESS => (typeof(VLESSInboundOptions), value.VLESSOptions),
                InboundTypes.TUIC => (typeof(TUICInboundOptions), value.TUICOptions),
                InboundTypes.Hysteria2 => (typeof(Hysteria2InboundOptions), value.Hysteria2Options),
                _ => throw new JsonException()
            };
            writer.WriteStartObject();
            foreach(var property in typeof(InboundOptions).GetProperties())
            {
                var propertyValue = property.GetValue(value);
                Utils.WriteProertyToJson(writer, propertyValue, typeof(InboundOptions), property.Name, options);
            }
            foreach(var property in optionType.GetProperties())
            {
                var propertyValue = property.GetValue(OoptionValue);
                Utils.WriteProertyToJson(writer, propertyValue, optionType, property.Name, options);
            }
            writer.WriteEndObject();
        }
    }
}
