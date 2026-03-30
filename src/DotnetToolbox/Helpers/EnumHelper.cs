using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DotnetToolbox.Helpers
{
    public static class EnumHelper
    {
        public static T Parse<T>(string value) where T : struct, Enum
        {
            return Enum.Parse<T>(value, ignoreCase: true);
        }

        public static T? TryParse<T>(string value) where T : struct, Enum
        {
            if (Enum.TryParse<T>(value, true, out var result))
                return result;
            return null;
        }

        public static IEnumerable<T> GetAll<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>();
        }

        public static string GetDescription<T>(T value) where T : struct, Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }

        public static Dictionary<int, string> ToDictionary<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>()
                .ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
        }
    }
}
