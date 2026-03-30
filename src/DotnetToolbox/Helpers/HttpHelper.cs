using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetToolbox.Helpers
{
    public static class HttpHelper
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<T> GetAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public static async Task<string> GetStringAsync(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<T> PostJsonAsync<T>(string url, object payload)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");
            var response = await _client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public static async Task<bool> IsReachableAsync(string url, int timeoutSeconds = 5)
        {
            try
            {
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutSeconds) };
                var response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static string BuildQueryString(params (string key, string value)[] parameters)
        {
            var sb = new StringBuilder("?");
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0) sb.Append("&");
                sb.Append(Uri.EscapeDataString(parameters[i].key));
                sb.Append("=");
                sb.Append(Uri.EscapeDataString(parameters[i].value));
            }
            return sb.ToString();
        }
    }
}
