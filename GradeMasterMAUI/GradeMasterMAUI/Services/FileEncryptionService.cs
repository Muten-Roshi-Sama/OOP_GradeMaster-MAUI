using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace GradeMasterMAUI.Services
{
    public static class FileEncryptionService
    {
        private static readonly string aesKeyName = "aesEncryptionKey";

        public static async Task EncryptFileAsync(string inputFile, string outputFile)
        {
            // Retrieve the stored AES key or create a new one if it doesn't exist
            string aesKeyBase64 = await SecureStorage.GetAsync(aesKeyName) ?? await GenerateAndStoreAesKey();

            byte[] aesKey = Convert.FromBase64String(aesKeyBase64);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;
                aesAlg.GenerateIV();

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    // Prepend the IV to the file
                    outputFileStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                        {
                            inputFileStream.CopyTo(csEncrypt);
                        }
                    }
                }
            }
        }

        public static async Task DecryptFileAsync(string inputFile, string outputFile)
        {
            string aesKeyBase64 = await SecureStorage.GetAsync(aesKeyName);
            if (aesKeyBase64 == null)
            {
                throw new InvalidOperationException("The AES key was not found in secure storage.");
            }

            byte[] aesKey = Convert.FromBase64String(aesKeyBase64);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;

                // Read the IV from the file
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                {
                    inputFileStream.Read(iv, 0, iv.Length);
                    aesAlg.IV = iv;

                    // Create a decryptor to perform the stream transform
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (CryptoStream csDecrypt = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                        {
                            csDecrypt.CopyTo(outputFileStream);
                        }
                    }
                }
            }
        }

        public static async Task<string> GenerateAndStoreAesKey()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey();
                string aesKeyBase64 = Convert.ToBase64String(aesAlg.Key);
                await SecureStorage.SetAsync(aesKeyName, aesKeyBase64);
                return aesKeyBase64;
            }
        }
    }

}




