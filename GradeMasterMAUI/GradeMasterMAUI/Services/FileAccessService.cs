using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;

namespace GradeMasterMAUI.Services
{
    public static class FileAccessService
    {
        private static readonly object FileLock = new();

        public static string ReadFile(string path, string origin)
        {
            lock (FileLock)
            {
                Config.EnsureDirectory();
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} was not found.[Origin : FileAccessService-{origin}]");
                }

                return File.ReadAllText(path);
            }
        }

        public static void WriteFile(string path, string content, string origin)
        {
            lock (FileLock)
            {
                File.WriteAllText(path, content);
            }
        }

        // Add other file operations as needed (e.g., DeleteFile, AppendToFile, etc.)
    }

}
