using System;

namespace DotnetToolbox.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool IsBetween(this double value, double min, double max)
        {
            return value >= min && value <= max;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static string ToOrdinal(this int num)
        {
            if (num <= 0) return num.ToString();

            int rem = num % 100;
            if (rem >= 11 && rem <= 13)
                return num + "th";

            switch (num % 10)
            {
                case 1: return num + "st";
                case 2: return num + "nd";
                case 3: return num + "rd";
                default: return num + "th";
            }
        }

        public static string ToPercentage(this double value, int decimals = 1)
        {
            return $"{Math.Round(value * 100, decimals)}%";
        }

        public static bool IsEven(this int value) => value % 2 == 0;
        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}
