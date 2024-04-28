using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using XrayGUI.Modle.SingboxOptions.Inbound;
using System.Text.Json.Nodes;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class ACMEDNS01ChallengeConverter : JsonConverter<ACMEDNS01ChallengeOptions>
    {
        public override ACMEDNS01ChallengeOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;
            var ACMEDNS01AliDNS = new ACMEDNS01ChallengeOptions();
            var obj = JsonNode.Parse(ref reader)?.AsObject() ?? throw new JsonException();
            string jsonName;
            jsonName = Utils.GetPropertyJsonName(ACMEDNS01AliDNS.GetType(), nameof(ACMEDNS01AliDNS.Provider));
            ACMEDNS01AliDNS.Provider = obj[jsonName]?.GetValue<string?>() ?? throw new JsonException();
            obj.Remove(jsonName);
            switch(ACMEDNS01AliDNS.Provider)
            {
                case DNSProviders.AliDNS:
                    ACMEDNS01AliDNS.AliDNSOptions = obj.Deserialize<ACMEDNS01AliDNSOptions>(options);
                    break;
                case DNSProviders.Cloudflare:
                    ACMEDNS01AliDNS.CloudflareOptions = obj.Deserialize<ACMEDNS01CloudflareOptions>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return ACMEDNS01AliDNS;
        }

        public override void Write(Utf8JsonWriter writer, ACMEDNS01ChallengeOptions value, JsonSerializerOptions options)
        {           
            (var ACMEDNS01ChallengeProvider, var ACMEDNS01ChallengeValue) =  value.Provider switch
            {
                DNSProviders.AliDNS => (typeof(ACMEDNS01AliDNSOptions),value.AliDNSOptions as object),
                DNSProviders.Cloudflare => (typeof(ACMEDNS01CloudflareOptions), value.CloudflareOptions),
                _ => throw new JsonException(),
            };
            writer.WriteStartObject();
            foreach(var p in typeof(ACMEDNS01ChallengeOptions).GetProperties())
            {
                var pvalue = p.GetValue(value);
                Utils.WriteProertyToJson(writer, pvalue, typeof(ACMEDNS01ChallengeOptions), p.Name, options);
            }
            foreach(var p in ACMEDNS01ChallengeProvider.GetProperties())
            {
                var pvalue = p.GetValue(ACMEDNS01ChallengeValue);
                Utils.WriteProertyToJson(writer, pvalue, ACMEDNS01ChallengeProvider, p.Name, options);
            }
            writer.WriteEndObject();
        }
    }
}
