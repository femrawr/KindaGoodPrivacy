using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Utils
{
    public class Crypto
    {
        private static readonly ulong[] primes = [
            2, 3, 5, 7,
            11, 13, 17,
            19, 23, 29,
            31, 37, 41,
            43, 47, 53
        ];

        public static string Encrypt(string raw, string key)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            string keyHash = ProcessKey(keyBytes);

            byte[] hashBytes = Convert.FromBase64String(keyHash);
            string vector = DeriveIV(hashBytes);

            byte[] vecBytes = Convert.FromBase64String(vector);
            byte[] rawBytes = Encoding.UTF8.GetBytes(raw);

            using (var aes = new AesGcm(hashBytes, 16))
            {
                byte[] cipherBytes = new byte[rawBytes.Length];
                byte[] tagBytes = new byte[16];

                aes.Encrypt(vecBytes, rawBytes, cipherBytes, tagBytes);

                byte[] joined = new byte[cipherBytes.Length + 28];
                Buffer.BlockCopy(vecBytes, 0, joined, 0, 12);
                Buffer.BlockCopy(cipherBytes, 0, joined, 12, cipherBytes.Length);
                Buffer.BlockCopy(tagBytes, 0, joined, 12 + cipherBytes.Length, 16);

                string encrypted = Convert.ToBase64String(joined);

                int rot = DeriveRot(
                    keyBytes.Length > 0 ? keyBytes[0] : 0,
                    keyBytes.Length > 15 ? keyBytes[15] : 0,
                    keyBytes.Length > 31 ? keyBytes[31] : 0
                );
                string rotated = Rotate(encrypted, rot);

                char[] chars = rotated.ToCharArray();
                Array.Reverse(chars);
                return new string(chars);
            }
        }

        public static string Decrypt(string enc, string key)
        {
            char[] chars = enc.ToCharArray();
            Array.Reverse(chars);
            string rearranged = new string(chars);

            byte[] keyBytes = Convert.FromBase64String(key);
            string keyHash = ProcessKey(keyBytes);

            byte[] hashBytes = Convert.FromBase64String(keyHash);

            int rot = DeriveRot(
                keyBytes.Length > 0 ? keyBytes[0] : 0,
                keyBytes.Length > 15 ? keyBytes[15] : 0,
                keyBytes.Length > 31 ? keyBytes[31] : 0
            );
            string unrotated = Rotate(rearranged, -rot);

            byte[] encrypted = Convert.FromBase64String(unrotated);

            byte[] iv = new byte[12];
            byte[] cipher = new byte[encrypted.Length - 28];
            byte[] tag = new byte[16];

            Buffer.BlockCopy(encrypted, 0, iv, 0, 12);
            Buffer.BlockCopy(encrypted, 12, cipher, 0, cipher.Length);
            Buffer.BlockCopy(encrypted, 12 + cipher.Length, tag, 0, 16);

            using (var aes = new AesGcm(hashBytes, 16))
            {
                byte[] plainBytes = new byte[cipher.Length];
                aes.Decrypt(iv, cipher, tag, plainBytes);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }

        private static string DeriveIV(byte[] key)
        {
            using (var sha512 = SHA512.Create())
            using (var sha256 = SHA256.Create())
            {
                byte[] hash512 = sha512.ComputeHash(key);
                byte[] hash256 = sha256.ComputeHash(key);

                byte[] mashedHashBytes = new byte[hash512.Length + hash256.Length];
                Buffer.BlockCopy(hash512, 0, mashedHashBytes, 0, hash512.Length);
                Buffer.BlockCopy(hash256, 0, mashedHashBytes, hash512.Length, hash256.Length);

                byte[] hashedMashedBytes = sha512.ComputeHash(mashedHashBytes);

                byte[] allHashBytes = new byte[
                    hash512.Length +
                    hash256.Length +
                    hashedMashedBytes.Length
                ];

                Buffer.BlockCopy(hash512, 0, allHashBytes, 0, hash512.Length);
                Buffer.BlockCopy(hash256, 0, allHashBytes, hash512.Length, hash256.Length);
                Buffer.BlockCopy(
                    hashedMashedBytes, 0,
                    allHashBytes, hash512.Length +
                    hash256.Length,
                    hashedMashedBytes.Length
                );

                for (int i = 2; i < allHashBytes.Length; i++)
                {
                    int fib = Math.Fibonacci(i % 20 + 1);
                    allHashBytes[i] = (byte)((
                        allHashBytes[i] ^
                        allHashBytes[i - 1] ^
                        allHashBytes[i - 2]
                   ) + (fib % 256));
                }

                ulong polynomial = 0;
                for (int i = 0; i < System.Math.Min(allHashBytes.Length, 16); i++)
                {
                    polynomial += (ulong)allHashBytes[i] *
                        primes[i] *
                        (ulong)System.Math.Pow(251, i % 8);
                }

                double x = (polynomial % 10000) / 10000.0;
                for (int i = 0; i < 1000; i++)
                    x = 3.987 * x * (1 - x);

                ulong chaos = (ulong)(x * ulong.MaxValue);
                byte[] chaosBytes = new byte[allHashBytes.Length];
                for (int i = 0; i < chaosBytes.Length; i++)
                {
                    chaos = (chaos * 1103515245 + 12345) % (1UL << 31);
                    chaosBytes[i] = (byte)(chaos % 256);
                }

                for (int i = 0; i < allHashBytes.Length; i++)
                    allHashBytes[i] ^= chaosBytes[i];

                byte[,] matrix = Math.GaloisMatrix(16, (int)(polynomial % 255) + 1);
                byte[] matrixResult = new byte[16];

                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        matrixResult[i] ^= Math.GaloisMultiply(matrix[i, j], allHashBytes[j % allHashBytes.Length]);
                    }
                }

                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    matrixResult, key.Take(16).ToArray(),
                    100000, HashAlgorithmName.SHA512
                    ))
                {
                    byte[] final = pbkdf2.GetBytes(12);
                    return Convert.ToBase64String(final);
                }
            }
        }

        private static string ProcessKey(byte[] key)
        {
            byte[] salt = new byte[32];

            using (var sha512 = SHA512.Create())
            {
                byte[] keyHash = sha512.ComputeHash(key);
                byte[] keyBytes = new byte[key.Length + 4];

                Buffer.BlockCopy(key, 0, keyBytes, 0, key.Length);
                Buffer.BlockCopy(
                    BitConverter.GetBytes(0x1A8F73C2),
                    0, keyBytes, key.Length, 4
                );

                byte[] byteHash = sha512.ComputeHash(keyBytes);

                for (int i = 0; i < salt.Length; i++)
                {
                    salt[i] = (byte)(
                        keyHash[i % keyHash.Length] ^
                        byteHash[i % byteHash.Length]
                    );
                }
            }

            byte[] processBytes = new byte[key.Length];
            Array.Copy(key, processBytes, key.Length);

            for (int i = 0; i < processBytes.Length; i++)
            {
                long prime = Math.GetPrime(i % 100 + 1);
                long modExp = Math.ModExponentiation(processBytes[i], 3, prime);

                int phi = Math.EulerTotient((int)prime);
                processBytes[i] = (byte)((modExp + phi) % 256);
            }

            byte[] sponged = new byte[200];
            Array.Copy(processBytes, sponged, System.Math.Min(processBytes.Length, 136));

            for (int i = 0; i < 24; i++)
                Math.SpongeTransform(sponged);

            using (var argon2 = new Argon2id(sponged))
            {
                argon2.Salt = salt;
                argon2.Iterations = 8;
                argon2.MemorySize = 65536;
                argon2.DegreeOfParallelism = Environment.ProcessorCount;

                byte[] hashedKey = argon2.GetBytes(32);

                for (int i = 0; i < hashedKey.Length; i += 2)
                {
                    if (i + 1 < hashedKey.Length)
                    {
                        long p = 5438034787;
                        long x = hashedKey[i];
                        long y = hashedKey[i + 1];

                        long s = ((3 * x * x + 1) * Math.ModInverse(2 * y, p)) % p;
                        long newX = (s * s - 2 * x) % p;
                        long newY = (s * (x - newX) - y) % p;

                        hashedKey[i] = (byte)(newX % 256);
                        hashedKey[i + 1] = (byte)(newY % 256);
                    }
                }

                return Convert.ToBase64String(hashedKey);
            }
        }

        private static string Rotate(string str, int len)
        {
            if (string.IsNullOrEmpty(str) || len == 0)
                return str;

            int modLen = len % str.Length;

            if (modLen < 0)
                modLen += str.Length;

            return str.Substring(modLen) + str.Substring(0, modLen);
        }

        private static int DeriveRot(int n1, int n2, int n3)
        {
            int product = 1;
            foreach (int prime in Math.GetPrimeFac(System.Math.Abs(n1) + 1))
                product = (product * prime) % 1000;

            int carmichael = Math.EulerTotient(System.Math.Abs(n2) + 1);
            int discreteLog = Math.DiscreteLog(System.Math.Abs(n3) % 100 + 1, 3, 101);

            int gcd = Math.GCD(System.Math.Abs(n1), System.Math.Abs(n2));
            long[] bezout = Math.ExtendedGCD(System.Math.Abs(n1), System.Math.Abs(n2));
            int diophantine = (int)(bezout[1] * System.Math.Abs(n3));

            int fibIndex = (System.Math.Abs(n1 + n2 + n3) % 30) + 1;
            int fib = Math.Fibonacci(fibIndex);
            int lucas = Math.LucasNumber(fibIndex);

            double phi = (1 + System.Math.Sqrt(5)) / 2;
            int[] convergent = Math.Convergent(phi, 10);
            int convergentSum = convergent[0] + convergent[1];

            double chaosParam = ((double)(n1 ^ n2 ^ n3) % 10000) / 10000.0;
            double chaosResult = chaosParam;

            for (int i = 0; i < 100; i++)
                chaosResult = 4.0 * chaosResult * (1.0 - chaosResult);

            int final = (
                product + carmichael + discreteLog +
                diophantine + fib + lucas + convergentSum +
                (int)(chaosResult * 1000)
            ) % 64;

            return final == 0 ? 7 : final;
        }
    }
}