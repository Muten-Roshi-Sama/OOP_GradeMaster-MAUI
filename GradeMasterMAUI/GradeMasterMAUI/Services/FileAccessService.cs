using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.IO;

namespace GradeMasterMAUI.Services
{
    public static class FileAccessService
    {
        //private static readonly object FileLock = new();
        private static readonly SemaphoreSlim FileSemaphore = new SemaphoreSlim(1, 1);

        public static async Task<string> ReadFileAsync(string path, string errorOrigin)
        {
            await FileSemaphore.WaitAsync();
            try
            {
                Config.EnsureDirectory();
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} was not found.[Origin : FileAccessService-{errorOrigin}]");
                }

                // Decrypt, Read, Encrypt
                await FileEncryptionService.DecryptFileAsync(inputFile: path, outputFile: path);
                string fileContent = File.ReadAllText(path);
                await FileEncryptionService.EncryptFileAsync(inputFile: path, outputFile: path);

                return fileContent;
            }
            finally
            {
                FileSemaphore.Release();
            }
        }

        public static async Task WriteFileAsync(string path, string content, string origin)
        {
            await FileSemaphore.WaitAsync();
            try
            {
                // Decrypt the file before writing.
                await FileEncryptionService.DecryptFileAsync(inputFile: path, outputFile: path);
                File.WriteAllText(path, content);
                // Encrypt the file after writing.
                await FileEncryptionService.EncryptFileAsync(inputFile: path, outputFile: path);
            }
            finally
            {
                FileSemaphore.Release();
            }
        }

        // Add other file operations as needed (e.g., DeleteFile, AppendToFile, etc.)
    }

}
