using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;

namespace XrayCoreConfigModle
{
    public static class JsonHandler
    {
        private static JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
        public static T? JsonDeserializeFromText<T>(string text)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(text, serializerOptions);
            }
            catch
            {
                return default;
            }
        }
        public static bool TryJsonDeserializeFromText<T>(string text_, out T outPut)
        {
            outPut = JsonDeserializeFromText<T>(text_)!;
            return outPut != null;

        }
        public static T? JsonDeserializeFromFile<T>(string path)
        {
            return JsonDeserializeFromText<T>(File.ReadAllText(path));
        }
        public static T? JsonDeserializeFromFile<T>(string path, Encoding encoding)
        {
            return JsonDeserializeFromText<T>(File.ReadAllText(path, encoding));
        }
        public static string JsonSerializeToString(object obj)
        {
            return JsonSerializer.Serialize(obj, serializerOptions);
        }
        public static void JsonSerializeToFile(object obj, string path)
        {
            JsonSerializeToFile(obj, path, Encoding.Default);
        }
        public static void JsonSerializeToFile(object obj, string path, Encoding encoding)
        {
            if(!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            }
            File.WriteAllText(path, JsonSerializeToString(obj), encoding);
        }

    }
}
