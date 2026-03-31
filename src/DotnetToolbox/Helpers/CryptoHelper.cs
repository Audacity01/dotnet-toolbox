using System;
using System.Security.Cryptography;
using System.Text;

namespace DotnetToolbox.Helpers
{
    public static class CryptoHelper
    {
        public static string HashSha256(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        public static string HashMd5(string input)
        {
            using var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        public static string GenerateToken(int length = 32)
        {
            var bytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string FromBase64(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }

        public static string GenerateApiKey(string prefix = "sk")
        {
            var bytes = new byte[24];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var key = Convert.ToBase64String(bytes).Replace("+", "").Replace("/", "").Replace("=", "");
            return $"{prefix}_{key}";
        }
    }
}
