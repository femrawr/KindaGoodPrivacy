using System.Text;

namespace KindaGoodPrivacy.Source.Utils.Crypto
{
    public class FastCrypt
    {
        public static string Encrypt(string raw, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var rawBytes = Encoding.UTF8.GetBytes(raw);

            var encBytes = new byte[rawBytes.Length];
            for (int i = 0; i < rawBytes.Length; i++)
                encBytes[i] = (byte)(rawBytes[i] ^
                    keyBytes[i % keyBytes.Length]
                );

            return Convert.ToBase64String(encBytes);
        }

        public static string Decrypt(string enc, string key)
        {
            var encBytes = Convert.FromBase64String(enc);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var decBytes = new byte[encBytes.Length];
            for (int i = 0; i < encBytes.Length; i++)
                decBytes[i] = (byte)(encBytes[i] ^
                    keyBytes[i % keyBytes.Length]
                );

            return Encoding.UTF8.GetString(decBytes);
        }

        public static string Hash(string data)
        {
            var str = new StringBuilder();
            foreach(var c in data)
            {
                char xored = (char)(c ^ (int)Math.Floor(
                    (double)(data.Length * 42) / 3)
                );

                str.Append(xored);
            }

            uint hash = 2166136261;
            foreach (var c in str.ToString())
            {
                hash ^= c;
                hash *= 16777619;
            }

            return hash.ToString("0");
        }
    }
}
