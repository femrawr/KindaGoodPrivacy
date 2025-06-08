using KindaGoodPrivacy.Source.Utils;
using System.Text;

namespace KindaGoodPrivacy.Source.Core
{
    public class CryptManager
    {
        public static string SetEncrypted(string data, string password)
        {
            string convData = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            string convPass = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            return Crypto.Encrypt(convData, convPass);
        }

        public static string SetDecrypted(string data, string password)
        {
            string convPass = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            string decrypted = Crypto.Decrypt(data, convPass);

            return Encoding.UTF8.GetString(Convert.FromBase64String(decrypted));
        }
    }
}
