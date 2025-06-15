using System.Text;

namespace KindaGoodPrivacy.Source.Utils
{
    public class Util
    {
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenString(int len)
        {
            var str = new StringBuilder(len);
            var ran = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < len; i++)
            {
                str.Append(chars[ran.Next(chars.Length)]);
            }

            return str.ToString();
        }
    }
}
