using IniParser;
using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Core
{
    public class LogIn
    {
        public static string? Login(string username, string password)
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(Utility.FileSystem.accountsFilePath);

            string usernameHash = Utility.Hash.Argon2id(username, password);
            string passwordHash = Utility.Hash.Argon2id(password, username);

            byte[] visibleHash = SHA256.HashData(Encoding.UTF8.GetBytes(usernameHash + passwordHash));
            string visibleHashString = Convert.ToBase64String(visibleHash).Replace("=", "");

            string finalHash = Utility.Hash.Argon2id(usernameHash, passwordHash).Replace("=", "");

            if (!data.Sections.ContainsSection("reg"))
            {
                data.Sections.AddSection("reg");
            }

            if (!data["reg"].ContainsKey(visibleHashString))
            {
                return "This account could not be found.";
            }

            if (!data.Sections.ContainsSection(data["reg"][visibleHashString]))
            {
                return "This account's data could not be found.";
            }

            string guid = data["reg"][visibleHashString];

            string testStringExpected = data[guid]["expected"];
            string testStringEncrypted = data[guid]["encrypted"];

            string testStringDecrypted = Utility.Crypt.DecryptAES(
                testStringEncrypted,
                usernameHash
            );

            if (testStringDecrypted != testStringExpected)
            {
                return "This account's data could not be verified.";
            }

            string keyPrimaryEncrypted = data[guid]["primary"];
            string keySecondaryEncrypted = data[guid]["secondary"];

            string keyPrimaryDecrypted = Utility.Crypt.DecryptAES(
                keyPrimaryEncrypted,
                usernameHash + passwordHash
            );

            string keySecondaryDecrypted = Utility.Crypt.DecryptAES(
                keySecondaryEncrypted,
                passwordHash + finalHash + keyPrimaryDecrypted
            );

            Store.key = Convert.ToBase64String(SHA512.HashData(Encoding.UTF8.GetBytes(
                keyPrimaryDecrypted +
                keySecondaryDecrypted +
                finalHash
            )));

            Store.path = Path.Join(
                Utility.FileSystem.accountsFolderPath,
                guid
            );

            return null;
        }
    }
}