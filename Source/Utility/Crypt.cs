using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Utility
{
    public class Crypt
    {
        public static string EncryptAES(string data, string key)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            byte[] hashBytes = SHA256.HashData(keyBytes);

            try
            {
                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = hashBytes;

                aes.GenerateIV();

                using var encryptor = aes.CreateEncryptor();
                byte[] encrypted = encryptor.TransformFinalBlock(
                    dataBytes,
                    0,
                    dataBytes.Length
                );

                byte[] final = new byte[aes.IV.Length + encrypted.Length];
                Buffer.BlockCopy(aes.IV, 0, final, 0, aes.IV.Length);
                Buffer.BlockCopy(encrypted, 0, final, aes.IV.Length, encrypted.Length);

                return Convert.ToBase64String(final);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(dataBytes);
                CryptographicOperations.ZeroMemory(keyBytes);
                CryptographicOperations.ZeroMemory(hashBytes);
            }
        }

        public static string DecryptAES(string data, string key)
        {
            byte[] encrypted = Convert.FromBase64String(data);

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] hashBytes = SHA256.HashData(keyBytes);

            try
            {
                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = hashBytes;

                byte[] iv = new byte[16];
                byte[] cipher = new byte[encrypted.Length - 16];
                Buffer.BlockCopy(encrypted, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(encrypted, iv.Length, cipher, 0, cipher.Length);

                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor();
                byte[] final = decryptor.TransformFinalBlock(
                    cipher,
                    0,
                    cipher.Length
                );

                return Encoding.UTF8.GetString(final);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(keyBytes);
                CryptographicOperations.ZeroMemory(hashBytes);
            }
        }
    }
}