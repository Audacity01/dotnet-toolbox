using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetToolbox.Helpers
{
    public class SimpleConfig
    {
        private readonly Dictionary<string, string> _values = new();
        private readonly string _filePath;

        public SimpleConfig(string filePath)
        {
            _filePath = filePath;
            if (File.Exists(filePath))
                Load();
        }

        private void Load()
        {
            foreach (var line in File.ReadAllLines(_filePath))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                    continue;

                var idx = trimmed.IndexOf('=');
                if (idx > 0)
                {
                    var key = trimmed.Substring(0, idx).Trim();
                    var value = trimmed.Substring(idx + 1).Trim();
                    _values[key] = value;
                }
            }
        }

        public string Get(string key, string defaultValue = null)
        {
            return _values.TryGetValue(key, out var val) ? val : defaultValue;
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            var val = Get(key);
            return int.TryParse(val, out var result) ? result : defaultValue;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            var val = Get(key);
            if (val == null) return defaultValue;
            return val.ToLower() == "true" || val == "1" || val.ToLower() == "yes";
        }

        public void Set(string key, string value)
        {
            _values[key] = value;
        }

        public void Save()
        {
            var lines = _values.Select(kvp => $"{kvp.Key}={kvp.Value}");
            File.WriteAllLines(_filePath, lines);
        }

        public bool HasKey(string key) => _values.ContainsKey(key);

        public IReadOnlyDictionary<string, string> GetAll() => _values;
    }
}
