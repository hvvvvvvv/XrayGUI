using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GlobalHotkey
{
    public class HotkeyConverter : JsonConverter<Hotkey>
    {
        public override Hotkey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {;
            Key key = default;
            KeyModifier keyModifier = default;
            if(reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            while(reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if(reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }
                switch(reader.GetString())
                {
                    case nameof(Hotkey.Key):
                        key = JsonSerializer.Deserialize<Key>(ref reader, options);
                        break;
                    case nameof(Hotkey.KeyModifier):
                        keyModifier = JsonSerializer.Deserialize<KeyModifier>(ref reader, options);
                        break;
                    default: reader.Skip();
                        break;
                }
            }
            return new(keyModifier, key);
        }

        public override void Write(Utf8JsonWriter writer, Hotkey value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach(var property in typeof(Hotkey).GetProperties())
            {
                writer.WritePropertyName(property.Name);
                JsonSerializer.Serialize(writer, property.GetValue(value), property.PropertyType, options);
            }
            writer.WriteEndObject();
        }
    }
}
