using System.Text;

namespace KindaGoodPrivacy.Source.Utility
{
    public class Random
    {
        private static readonly string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";

        public static string GenString(int len)
        {
            var builder = new StringBuilder(len);
            var random = new System.Random();

            for (int i = 0; i < len; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }
}
