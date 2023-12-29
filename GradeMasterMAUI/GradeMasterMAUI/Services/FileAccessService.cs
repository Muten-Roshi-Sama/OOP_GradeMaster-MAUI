using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Services
{
    public static class FileAccessService
    {
        private static readonly object FileLock = new object();

        public static string ReadFile(string path)
        {
            lock (FileLock)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} was not found.");
                }

                return File.ReadAllText(path);
            }
        }

        public static void WriteFile(string path, string content)
        {
            lock (FileLock)
            {
                File.WriteAllText(path, content);
            }
        }

        // Add other file operations as needed (e.g., DeleteFile, AppendToFile, etc.)
    }

}
