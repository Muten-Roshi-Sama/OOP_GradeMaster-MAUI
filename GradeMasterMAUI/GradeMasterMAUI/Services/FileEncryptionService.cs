using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Maui.Storage;

public static class FileEncryptionService
{
    private static readonly string KeyName = "AesEncryptionKey";

    public static void InitializeEncryptionKey()
    {
        if (SecureStorage.GetAsync(KeyName).Result == null)
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                string base64Key = Convert.ToBase64String(aes.Key);
                SecureStorage.SetAsync(KeyName, base64Key).Wait();
            }
        }
    }   //TODO: add this to Config.EnsureDir()

    private static byte[] GetAesKey()
    {
        string base64Key = SecureStorage.GetAsync(KeyName).Result;
        return Convert.FromBase64String(base64Key);
    }

    public static string EncryptText(string text)
    {
        byte[] key = GetAesKey();
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(text);
                }

                byte[] iv = aes.IV;
                byte[] encrypted = ms.ToArray();
                byte[] result = new byte[iv.Length + encrypted.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

                return Convert.ToBase64String(result);
            }
        }
    }

    public static string DecryptText(string cipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);
        byte[] iv = new byte[16];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        using (var aes = Aes.Create())
        {
            aes.Key = GetAesKey();
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var ms = new MemoryStream(cipher))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
