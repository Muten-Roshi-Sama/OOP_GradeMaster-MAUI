



namespace GradeMasterMAUI.Services
{
    public static class FileAccessService
    {
        //private static readonly object FileLock = new();

        public static string ReadFile(string path, string errorOrigin)
        {
            //lock (FileLock)
            //{
                Config.EnsureDirAndAesKey();
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"The file at {path} was not found.[Origin : FileAccessService-{errorOrigin}]");
                }

                string encryptedContent = File.ReadAllText(path);
                return FileEncryptionService.DecryptText(encryptedContent);
            //}
        }

        public static void WriteFile(string path, string content, string identifier, string errorOrigin)
        {
            //lock (FileLock)
            //{
                Config.EnsureDirAndAesKey();
                //string directory = Path.GetDirectoryName(path);
                //string filenameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                //string extension = Path.GetExtension(path);

                //// Append the identifier to the filename
                //string newFileName = $"{filenameWithoutExtension}.{identifier}{extension}";
                //string newPath = Path.Combine(directory, newFileName);

                string encryptedContent = FileEncryptionService.EncryptText(content);
                File.WriteAllText(path, encryptedContent);
            //}
        }
    }

}