using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComputerSystem.Common
{
    public static class Serializer
    {
        private static JsonSerializerOptions GetOptions()
        {
            var o = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            o.Converters.Add(new JsonStringEnumConverter());
            return o;
        }

        public static async Task SaveToFileAsync<T>(T obj, string path)
        {
            var options = GetOptions();
            var json = JsonSerializer.Serialize(obj, options);
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllTextAsync(path, json).ConfigureAwait(false);
        }

        public static async Task<T?> LoadFromFileAsync<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            var options = GetOptions();
            var json = await File.ReadAllTextAsync(path).ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static void SaveToFile<T>(T obj, string path) =>
            SaveToFileAsync(obj, path).GetAwaiter().GetResult();

        public static T? LoadFromFile<T>(string path) =>
            LoadFromFileAsync<T>(path).GetAwaiter().GetResult();
    }
}
