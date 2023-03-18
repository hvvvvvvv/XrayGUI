using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;
using static System.Net.WebRequestMethods;

namespace XrayCoreConfigModle.JsonConverters
{
    internal class InboundServerConverter : JsonConverter<InboundServerItemObject>
    {
        public override InboundServerItemObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var ret = new InboundServerItemObject();            
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    var propertyInfo = ret.GetType().GetProperties().FirstOrDefault(i => i.Name == propertyName);    
                    
                    reader.Read();
                    if(propertyInfo != null)
                    {
                        var propertyValue = JsonSerializer.Deserialize(ref reader, propertyInfo.PropertyType, options);
                        propertyInfo.SetValue(ret, propertyValue);
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }
            if (ret.settings is UnknownConfigurationObject unknownTypeSetting)
            {
                ret.settings = ret.protocol switch
                {
                    "socks" => unknownTypeSetting.ConverToSpecificType<SocksConfigurationObject>(),
                    "http" => unknownTypeSetting.ConverToSpecificType<HttpConfigurationObject>(),
                    "dokodemo-door" => unknownTypeSetting.ConverToSpecificType<DokodemoDoorConfigurationObject>(),
                    "shadowsocks" => unknownTypeSetting.ConverToSpecificType<ShadowsocksConfigurationObject>(),
                    "trojan" => unknownTypeSetting.ConverToSpecificType<TrojanConfigurationObject>(),
                    "vless" => unknownTypeSetting.ConverToSpecificType<VlessConfigurationObject>(),
                    "vmess" => unknownTypeSetting.ConverToSpecificType<VMessConfigurationObject>(),
                    _ => unknownTypeSetting
                };
            }
            return ret;
        }

        public override void Write(Utf8JsonWriter writer, InboundServerItemObject value, JsonSerializerOptions options)
        {
            if(value is null)
            {
                return;
            }
            writer.WriteStartObject();
            foreach(var propertyInfo in value.GetType().GetProperties())
            {
                var propertValue = propertyInfo.GetValue(value);
                if (propertValue != null)
                {
                    writer.WritePropertyName(propertyInfo.Name);
                    JsonSerializer.Serialize(writer, propertValue, propertyInfo.PropertyType, options);
                }
            }
            writer.WriteEndObject();
        }
    }
}
