using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComputerSystem.Common
{
    public static class Serializer
    {
        private static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public static void SaveToFile<T>(T obj, string path)
        {
            var options = GetOptions();
            var json = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(path, json);
        }

        public static T LoadFromFile<T>(string path)
        {
            if (!File.Exists(path)) return default(T);
            
            var options = GetOptions();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}