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
    public class RuleSetConverter : JsonConverter<RuleSetOptions>
    {
        public override RuleSetOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            var obj = JsonNode.Parse(ref reader)?.AsObject() ?? throw new JsonException();
            var ruleSetOptions = new RuleSetOptions();
            string jsonName;

            jsonName = Utils.GetPropertyJsonName(ruleSetOptions.GetType(), nameof(ruleSetOptions.Type));
            ruleSetOptions.Type = obj[jsonName]?.GetValue<string>() ?? throw new JsonException();
            obj.Remove(jsonName);

            jsonName = Utils.GetPropertyJsonName(ruleSetOptions.GetType(), nameof(ruleSetOptions.Tag));
            ruleSetOptions.Tag = obj[jsonName]?.GetValue<string>() ?? throw new JsonException();
            obj.Remove(jsonName);

            jsonName = Utils.GetPropertyJsonName(ruleSetOptions.GetType(), nameof(ruleSetOptions.Format));
            ruleSetOptions.Format = obj[jsonName]?.GetValue<string>() ?? throw new JsonException();
            obj.Remove(jsonName);
            switch(ruleSetOptions.Type)
            {
                case RuleSetTypes.Local:
                    ruleSetOptions.LocalRuleSet = obj.Deserialize<LocalRuleSetOptions>(options);
                    break;
                case RuleSetTypes.Remote:
                    ruleSetOptions.RemoteRuleSet = obj.Deserialize<RemoteRuleSetOptions>(options);
                    break;
                default:
                    throw new JsonException();
            }
            return ruleSetOptions;
        }

        public override void Write(Utf8JsonWriter writer, RuleSetOptions value, JsonSerializerOptions options)
        {
            (var RuleSetType, var RuleSetValue) = value.Type switch
            {
                RuleSetTypes.Local => (typeof(LocalRuleSetOptions), value.LocalRuleSet as object),
                RuleSetTypes.Remote => (typeof(RemoteRuleSetOptions), value.RemoteRuleSet),
                _ => throw new JsonException(),
            };
            writer.WriteStartObject();

            foreach(var p in typeof(RuleSetOptions).GetProperties())
            {
                var pvalue = p.GetValue(value);
                Utils.WriteProertyToJson(writer, pvalue, typeof(RuleSetOptions), p.Name, options);
            }
            foreach(var p in RuleSetType.GetProperties())
            {
                var pvalue = p.GetValue(RuleSetValue);
                Utils.WriteProertyToJson(writer, pvalue, RuleSetType, p.Name, options);
            }
            writer.WriteEndObject();
        }
    }
}
