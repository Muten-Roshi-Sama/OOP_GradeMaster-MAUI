



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

                string encryptedContent = File.ReadAllText(path);
                return FileEncryptionService.DecryptText(encryptedContent);
            }
        }

        public static void WriteFile(string path, string content, string errorOrigin)
        {
            lock (FileLock)
            {
                string encryptedContent = FileEncryptionService.EncryptText(content);
                File.WriteAllText(path, encryptedContent);
            }
        }
    }

}