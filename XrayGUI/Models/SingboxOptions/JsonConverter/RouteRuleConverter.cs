using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Route;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public class RouteRuleConverter : JsonConverter<RuleOptions>
    {
        public override RuleOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Null) return null;
            var ruleOptions = new RuleOptions();
            var obj = JsonNode.Parse(ref reader)?.AsObject() ?? throw new JsonException();
            string jsonName;
            jsonName = Utils.GetPropertyJsonName(ruleOptions.GetType(), nameof(ruleOptions.Type));
            ruleOptions.Type = obj[jsonName]?.GetValue<string?>();
            obj.Remove(jsonName);
            switch(ruleOptions.Type)
            {
                case RuleTypes.Default:
                case "":
                case null:
                    ruleOptions.DefaultRuleOtions = obj.Deserialize<DefaultRule>(options);
                    break;
                case RuleTypes.Logical:
                    ruleOptions.LogicalRuleOptions = obj.Deserialize<LogicalRule>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return ruleOptions;
        }

        public override void Write(Utf8JsonWriter writer, RuleOptions value, JsonSerializerOptions options)
        {
            
            (var RuleType, var RuleValue) =  value.Type switch
            {
                RuleTypes.Default or "" or null => (typeof(DefaultRule),value.DefaultRuleOtions as object),
                RuleTypes.Logical => (typeof(LogicalRule), value.LogicalRuleOptions),
                _ => throw new JsonException(),
            };
            writer.WriteStartObject();
            foreach(var p in typeof(RuleOptions).GetProperties())
            {
                var pvalue = p.GetValue(value);
                Utils.WriteProertyToJson(writer, pvalue, typeof(RuleOptions), p.Name, options);
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
