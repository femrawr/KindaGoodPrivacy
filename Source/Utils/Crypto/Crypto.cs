using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Utils.Crypto
{
    public class Crypto
    {
        private const int KEY_SIZE = 32;
        private const int NONCE_SIZE = 12;
        private const int TAG_SIZE = 16;
        private const int SALT_SIZE = 32;

        public static string Encrypt(string raw, string pass)
        {
            byte[] rawBytes = Encoding.UTF8.GetBytes(raw);

            byte[] salt = new byte[SALT_SIZE];
            byte[] nonce = new byte[NONCE_SIZE];

            RandomNumberGenerator.Fill(salt);
            RandomNumberGenerator.Fill(nonce);

            byte[] key = DeriveKey(pass, salt);

            byte[] ciphered = new byte[rawBytes.Length];
            byte[] tag = new byte[TAG_SIZE];

            using var aes = new AesGcm(key, TAG_SIZE);
            aes.Encrypt(nonce, rawBytes, ciphered, tag);

            byte[] encrypted = new byte[
                ciphered.Length +
                NONCE_SIZE +
                TAG_SIZE +
                SALT_SIZE
            ];

            int offset = 0;

            Buffer.BlockCopy(ciphered, 0, encrypted, offset, ciphered.Length);
            offset += ciphered.Length;

            Buffer.BlockCopy(nonce, 0, encrypted, offset, NONCE_SIZE);
            offset += NONCE_SIZE;

            Buffer.BlockCopy(tag, 0, encrypted, offset, TAG_SIZE);
            offset += TAG_SIZE;

            Buffer.BlockCopy(salt, 0, encrypted, offset, SALT_SIZE);

            try
            {
                return Convert.ToBase64String(encrypted);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(rawBytes);
                CryptographicOperations.ZeroMemory(salt);
                CryptographicOperations.ZeroMemory(nonce);
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(ciphered);
                CryptographicOperations.ZeroMemory(tag);
                CryptographicOperations.ZeroMemory(encrypted);
            }
        }

        public static string Decrypt(string enc, string pass)
        {
            byte[] encrypted = Convert.FromBase64String(enc);

            int size = NONCE_SIZE + TAG_SIZE + SALT_SIZE;
            int encLen = encrypted.Length - size;

            if (encrypted.Length < size)
                throw new ArgumentException("Invalid data");

            byte[] ciphered = new byte[encLen];
            byte[] tag = new byte[TAG_SIZE];

            byte[] salt = new byte[SALT_SIZE];
            byte[] nonce = new byte[NONCE_SIZE];

            int offset = 0;

            Buffer.BlockCopy(encrypted, offset, ciphered, 0, encLen);
            offset += encLen;

            Buffer.BlockCopy(encrypted, offset, nonce, 0, NONCE_SIZE);
            offset += NONCE_SIZE;

            Buffer.BlockCopy(encrypted, offset, tag, 0, TAG_SIZE);
            offset += TAG_SIZE;

            Buffer.BlockCopy(encrypted, offset, salt, 0, SALT_SIZE);

            byte[] key = DeriveKey(pass, salt);
            byte[] decrypted = new byte[ciphered.Length];

            using var aes = new AesGcm(key, TAG_SIZE);
            aes.Decrypt(nonce, ciphered, tag, decrypted);

            try
            {
                return Encoding.UTF8.GetString(decrypted);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(encrypted);
                CryptographicOperations.ZeroMemory(ciphered);
                CryptographicOperations.ZeroMemory(tag);
                CryptographicOperations.ZeroMemory(salt);
                CryptographicOperations.ZeroMemory(nonce);
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(decrypted);
            }
        }

        private static byte[] DeriveKey(string pass, byte[] salt)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(pass);

            try
            {
                using var argon2 = new Argon2id(passBytes);
                argon2.Salt = salt;
                argon2.Iterations = 35;
                argon2.MemorySize = 65536;
                argon2.DegreeOfParallelism = Environment.ProcessorCount;

                return argon2.GetBytes(KEY_SIZE);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(passBytes);
            }
        }
    }
}