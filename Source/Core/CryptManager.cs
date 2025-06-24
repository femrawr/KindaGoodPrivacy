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

        public static (string, bool) SetDecrypted(string data, string password)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            string convPass = Convert.ToBase64String(passBytes);

            string decrypted = "";
            try
            {
                decrypted = Crypto.Decrypt(data, convPass);
            }
            catch (FormatException e)
            {
                var err = new Error.ErrorInfo
                {
                    CustomMessage = "A formatting error has occurred.",
                    ErrorType = e.GetType().Name,
                    ErrorMessage = e.Message,
                    StackTrace = e.StackTrace
                };

                MessageBox.Show(
                    err.ToString(),
                    "An error occurred",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return ("", false);
            }
            catch (ArgumentException e)
            {
                var err = new Error.ErrorInfo
                {
                    CustomMessage = "An argument error has occurred.",
                    ErrorType = e.GetType().Name,
                    ErrorMessage = e.Message,
                    StackTrace = e.StackTrace
                };

                MessageBox.Show(
                    err.ToString(),
                    "An error occurred",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return ("", false);
            }
            catch (Exception e)
            {
                var err = new Error.ErrorInfo
                {
                    CustomMessage = "An unexpected error has occurred.",
                    ErrorType = e.GetType().Name,
                    ErrorMessage = e.Message,
                    StackTrace = e.StackTrace
                };

                MessageBox.Show(
                    err.ToDebugString(),
                    "An unexpected error occurred",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return ("", false);
            }

            byte[] decBytes = Convert.FromBase64String(decrypted);

            try
            {
                return (Encoding.UTF8.GetString(decBytes), true);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(passBytes);
                CryptographicOperations.ZeroMemory(decBytes);
            }
        }
    }
}
