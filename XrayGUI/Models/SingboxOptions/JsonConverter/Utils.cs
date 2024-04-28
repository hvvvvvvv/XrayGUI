using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.Subscription;
using static System.Resources.ResXFileRef;

namespace XrayGUI.Modle.SingboxOptions.JsonConverter
{
    public static class Utils
    {
        public static string GetPropertyJsonName(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);
            if(property == null)
            {
                throw new ArgumentException($"Property {propertyName} not found in type {type.FullName}");
            }
            var attribute = property.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false).FirstOrDefault();
            return (attribute as JsonPropertyNameAttribute)?.Name ?? propertyName;
        }
        public static System.Text.Json.Serialization.JsonConverter? GetProertyJsonConverter(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName) ?? throw new ArgumentException($"Property {propertyName} not found in type {type.FullName}");
            var attribute = property.GetCustomAttributes(typeof(JsonConverterAttribute), false).FirstOrDefault();
            if (attribute is JsonConverterAttribute jsonConverterAttribute)
            {
                if (jsonConverterAttribute.ConverterType != null)
                {
                    if (Activator.CreateInstance(jsonConverterAttribute.ConverterType) is System.Text.Json.Serialization.JsonConverter converter)
                    {
                        return converter;
                    }
                }
            }
            return null;
        }

        public static void WriteProertyToJson(Utf8JsonWriter writer, object? value, Type type, string propertyName, JsonSerializerOptions options)
        {
            var isIgnore = PropertyIsIgnored(type, propertyName);
            if((value == null && options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull) || isIgnore)
            {
                return;
            }           
            var property = type.GetProperty(propertyName) ?? throw new ArgumentException($"Property {propertyName} not found in type {type.FullName}");
            var converter = GetProertyJsonConverter(type, propertyName);
            var optionCopy = new JsonSerializerOptions(options);
            optionCopy.Converters.Clear();
            if (converter != null)
            {               
                optionCopy.Converters.Add(converter);
            }
            
            writer.WritePropertyName(GetPropertyJsonName(type, propertyName));
            JsonSerializer.Serialize(writer, value, property.PropertyType, optionCopy);
        }


        public static bool PropertyIsIgnored(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName) ?? throw new ArgumentException($"Property {propertyName} not found in type {type.FullName}");
            return property.GetCustomAttribute<JsonIgnoreAttribute>() != null;
        }
    }
}
