using KindaGoodPrivacy.Source.Utils.Crypto;
using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Core
{
    public class CryptManager
    {
        public static string SetEncrypted(string data, string password)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] passBytes = Encoding.UTF8.GetBytes(password);

            string convData = Convert.ToBase64String(dataBytes);
            string convPass = Convert.ToBase64String(passBytes);

            try
            {
                return Crypto.Encrypt(convData, convPass);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(dataBytes);
                CryptographicOperations.ZeroMemory(passBytes);
            }
        }

        public static string SetDecrypted(string data, string password)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(password);

            string convPass = Convert.ToBase64String(passBytes);
            string decrypted = Crypto.Decrypt(data, convPass);

            byte[] decBytes = Convert.FromBase64String(decrypted);

            try
            {
                return Encoding.UTF8.GetString(decBytes);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(passBytes);
                CryptographicOperations.ZeroMemory(decBytes);
            }
        }
    }
}
