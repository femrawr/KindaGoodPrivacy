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

            byte[] salt = DeriveSalt(pass);
            byte[] key = DeriveKey(pass, salt);
            byte[] nonce = DeriveNonce(key, salt);

            byte[] ciphered = new byte[rawBytes.Length];
            byte[] tag = new byte[TAG_SIZE];

            using var aes = new AesGcm(key, TAG_SIZE);
            aes.Encrypt(nonce, rawBytes, ciphered, tag);

            byte[] encrypted = new byte[ciphered.Length + TAG_SIZE];
            Buffer.BlockCopy(ciphered, 0, encrypted, 0, ciphered.Length);
            Buffer.BlockCopy(tag, 0, encrypted, ciphered.Length, TAG_SIZE);

            try
            {
                return Convert.ToBase64String(encrypted);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(rawBytes);
                CryptographicOperations.ZeroMemory(salt);
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(nonce);
                CryptographicOperations.ZeroMemory(ciphered);
                CryptographicOperations.ZeroMemory(tag);
                CryptographicOperations.ZeroMemory(encrypted);
            }
        }

        public static string Decrypt(string enc, string pass)
        {
            byte[] encrypted = Convert.FromBase64String(enc);

            byte[] ciphered = new byte[encrypted.Length - TAG_SIZE];
            byte[] tag = new byte[TAG_SIZE];

            Buffer.BlockCopy(encrypted, 0, ciphered, 0, ciphered.Length);
            Buffer.BlockCopy(encrypted, ciphered.Length, tag, 0, TAG_SIZE);

            byte[] salt = DeriveSalt(pass);
            byte[] key = DeriveKey(pass, salt);
            byte[] nonce = DeriveNonce(key, salt);

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
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(nonce);
                CryptographicOperations.ZeroMemory(decrypted);
            }
        }

        private static byte[] DeriveSalt(string pass)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(pass);

            byte[] joined = new byte[passBytes.Length * 2];
            Buffer.BlockCopy(passBytes, 0, joined, 0, passBytes.Length);
            Buffer.BlockCopy(passBytes, 0, joined, passBytes.Length, passBytes.Length);

            try
            {
                using var hmac = new HMACSHA512(passBytes);
                byte[] hash = hmac.ComputeHash(joined);

                byte[] salt = new byte[SALT_SIZE];
                Buffer.BlockCopy(hash, 0, salt, 0, SALT_SIZE);

                return salt;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(passBytes);
                CryptographicOperations.ZeroMemory(joined);
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
                CryptographicOperations.ZeroMemory(salt);
            }
        }

        private static byte[] DeriveNonce(byte[] key, byte[] salt)
        {
            byte[] joined = new byte[key.Length + salt.Length + key.Length];
            int offset = 0;

            Buffer.BlockCopy(key, 0, joined, offset, key.Length);
            offset += key.Length;

            Buffer.BlockCopy(salt, 0, joined, offset, salt.Length);
            offset += salt.Length;

            Buffer.BlockCopy(key, 0, joined, offset, key.Length);

            try
            {
                using var sha512 = SHA512.Create();
                byte[] hash = sha512.ComputeHash(joined);

                byte[] nonce = new byte[NONCE_SIZE];
                Buffer.BlockCopy(hash, 0, nonce, 0, NONCE_SIZE);

                return nonce;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(salt);
                CryptographicOperations.ZeroMemory(joined);
            }
        }
    }
}