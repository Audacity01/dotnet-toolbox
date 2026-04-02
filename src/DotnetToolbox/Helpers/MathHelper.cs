using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetToolbox.Helpers
{
    public static class MathHelper
    {
        public static double Average(params double[] values)
        {
            if (values.Length == 0) return 0;
            return values.Average();
        }

        public static double Median(double[] values)
        {
            if (values.Length == 0) return 0;
            var sorted = values.OrderBy(x => x).ToArray();
            int mid = sorted.Length / 2;
            return sorted.Length % 2 == 0
                ? (sorted[mid - 1] + sorted[mid]) / 2.0
                : sorted[mid];
        }

        public static double StandardDeviation(double[] values)
        {
            if (values.Length <= 1) return 0;
            double avg = values.Average();
            double sumSquares = values.Sum(v => (v - avg) * (v - avg));
            return Math.Sqrt(sumSquares / (values.Length - 1));
        }

        public static long Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("n must be non-negative");
            long result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }

        public static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int LCM(int a, int b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        public static bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0 || n % 3 == 0) return false;
            for (int i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0) return false;
            }
            return true;
        }

        public static double Map(double value, double fromMin, double fromMax, double toMin, double toMax)
        {
            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }
    }
}
