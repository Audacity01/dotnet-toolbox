using System;
using System.IO;

namespace DotnetToolbox.Helpers
{
    public static class FileUtils
    {
        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }

        public static string ReadFileSafe(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool WriteFileSafe(string path, string content)
        {
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(path, content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetTempFilePath(string extension = ".tmp")
        {
            return Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{extension}");
        }
    }
}
