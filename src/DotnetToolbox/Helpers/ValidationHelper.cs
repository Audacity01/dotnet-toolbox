using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotnetToolbox.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var result)
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            var cleaned = Regex.Replace(phone, @"[\s\-\(\)]", "");
            return Regex.IsMatch(cleaned, @"^\+?\d{10,15}$");
        }

        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;
            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));
            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        public static bool IsValidIpAddress(string ip)
        {
            var parts = ip.Split('.');
            if (parts.Length != 4) return false;
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int num)) return false;
                if (num < 0 || num > 255) return false;
                if (part.Length > 1 && part.StartsWith("0")) return false;
            }
            return true;
        }

        public static bool IsValidCreditCard(string number)
        {
            var digits = number.Replace(" ", "").Replace("-", "");
            if (!digits.All(char.IsDigit) || digits.Length < 13 || digits.Length > 19)
                return false;

            int sum = 0;
            bool alternate = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int n = digits[i] - '0';
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
    }
}
