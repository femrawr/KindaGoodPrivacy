using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Utility
{
    public class Hash
    {
        public static string Argon2id(
            string data,
            string salt,
            int iterations = 10,
            int threads = 4,
            int memoryInKB = 65536,
            int returnBytes = 64
        )
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            try
            {
                using var argon2 = new Argon2id(dataBytes);
                argon2.Salt = saltBytes;
                argon2.Iterations = iterations;
                argon2.DegreeOfParallelism = threads;
                argon2.MemorySize = memoryInKB;

                byte[] hashBytes = argon2.GetBytes(returnBytes);
                return Convert.ToBase64String(hashBytes);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(dataBytes);
                CryptographicOperations.ZeroMemory(saltBytes);
            }
        }
    }
}