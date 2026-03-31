using System;
using System.Text.Json;

namespace DotnetToolbox.Helpers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions PrettyOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static readonly JsonSerializerOptions CompactOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string Serialize<T>(T obj, bool pretty = true)
        {
            return JsonSerializer.Serialize(obj, pretty ? PrettyOptions : CompactOptions);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public static T DeserializeSafe<T>(string json, T defaultValue = default)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool IsValid(string json)
        {
            try
            {
                JsonDocument.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetValue(string json, string propertyName)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty(propertyName, out var element))
                    return element.ToString();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static string Merge(string json1, string json2)
        {
            using var doc1 = JsonDocument.Parse(json1);
            using var doc2 = JsonDocument.Parse(json2);

            var merged = new System.Collections.Generic.Dictionary<string, JsonElement>();

            foreach (var prop in doc1.RootElement.EnumerateObject())
                merged[prop.Name] = prop.Value.Clone();

            foreach (var prop in doc2.RootElement.EnumerateObject())
                merged[prop.Name] = prop.Value.Clone();

            return JsonSerializer.Serialize(merged, PrettyOptions);
        }
    }
}
