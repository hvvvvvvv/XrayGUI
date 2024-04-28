using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Dns;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class DNSRuleConverter : JsonConverter<DNSRuleOptions>
    {
        public override DNSRuleOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;
            var obj = JsonNode.Parse(ref reader)?.AsObject() ?? throw new JsonException();
            var dnsRuleOptions = new DNSRuleOptions();
            string jsonName;
            jsonName = Utils.GetPropertyJsonName(dnsRuleOptions.GetType(), nameof(dnsRuleOptions.Type));
            dnsRuleOptions.Type = obj[jsonName]?.GetValue<string?>();
            obj.Remove(jsonName);
            switch(dnsRuleOptions.Type)
            {
                case RuleTypes.Default:
                case "":
                case null:
                    dnsRuleOptions.DefaultOption = obj.Deserialize<DefaultDNSRule>(options);
                    break;
                case RuleTypes.Logical:
                    dnsRuleOptions.LogicalOption = obj.Deserialize<LogicalDNSRule>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return dnsRuleOptions;
        }

        public override void Write(Utf8JsonWriter writer, DNSRuleOptions value, JsonSerializerOptions options)
        {            
            (var RuleType, var RuleValue) =  value.Type switch
            {
                RuleTypes.Default or "" or null => (typeof(DefaultDNSRule),value.DefaultOption as object),
                RuleTypes.Logical => (typeof(LogicalDNSRule), value.LogicalOption),
                _ => throw new JsonException(),
            };
            writer.WriteStartObject();
            foreach(var p in typeof(DNSRuleOptions).GetProperties())
            {
                var pvalue = p.GetValue(value);
                Utils.WriteProertyToJson(writer, pvalue, typeof(DNSRuleOptions), p.Name, options);
            }
            foreach(var p in RuleType.GetProperties())
            {
                var pvalue = p.GetValue(RuleValue);
                Utils.WriteProertyToJson(writer, pvalue, RuleType, p.Name, options);
            }
            writer.WriteEndObject();
        }
    }
}
