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

        public static string ReadFile(string path, string errorOrigin)
        {
            lock (FileLock)
            {
                Config.EnsureDirectory();
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} was not found.[Origin : FileAccessService-{errorOrigin}]");
                }

                //FileEncryptionService.EncryptFileAsync(inputFile: path, outputFile: path);
                //string file = 
                //FileEncryptionService.DecryptFileAsync(inputFile: path, outputFile: path);

                return File.ReadAllText(path); ;
            }
        }

        public static void WriteFile(string path, string content, string origin)
        {
            lock (FileLock)
            {
                //FileEncryptionService.EncryptFileAsync(inputFile: path, outputFile: path);
                File.WriteAllText(path, content);
                //FileEncryptionService.DecryptFileAsync(inputFile: path, outputFile: path);
            }
        }

        // Add other file operations as needed (e.g., DeleteFile, AppendToFile, etc.)
    }

}