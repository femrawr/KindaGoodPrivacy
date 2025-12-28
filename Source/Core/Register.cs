using IniParser;
using System.Security.Cryptography;
using System.Text;

namespace KindaGoodPrivacy.Source.Core
{
    public class Register
    {
        public static string? Create(string username, string password)
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

            if (data["reg"].ContainsKey(visibleHashString))
            {
                return "This account already exists.";
            }

            string guid = Guid.NewGuid().ToString();

            data["reg"][visibleHashString] = guid;
            
            if (data.Sections.ContainsSection(guid))
            {
                return "WTFFF!!! This GUID is already in use\nClick the \"Create\" button again.";
            }

            data.Sections.AddSection(guid);

            string testString = Utility.Random.GenString(24);
            string testStringEncrypted = Utility.Crypt.EncryptAES(
                testString,
                usernameHash
            );

            string realKeyPrimary = Utility.Random.GenString(67);

            string keyPrimary = Utility.Crypt.EncryptAES(
                realKeyPrimary,
                usernameHash + passwordHash
            );

            string keySecondary = Utility.Crypt.EncryptAES(
                Utility.Random.GenString(37),
                passwordHash + finalHash + realKeyPrimary
            );

            data[guid]["expected"] = testString;
            data[guid]["encrypted"] = testStringEncrypted;
            data[guid]["primary"] = keyPrimary;
            data[guid]["secondary"] = keySecondary;

            parser.WriteFile(Utility.FileSystem.accountsFilePath, data);

            string accountFolder = Path.Join(
                Utility.FileSystem.accountsFolderPath,
                guid
            );

            if (!Directory.Exists(accountFolder))
            {
                Directory.CreateDirectory(accountFolder);
            }

            Directory.CreateDirectory(Path.Join(accountFolder, "Temp"));
            Directory.CreateDirectory(Path.Join(accountFolder, "Text"));
            Directory.CreateDirectory(Path.Join(accountFolder, "Image"));
            Directory.CreateDirectory(Path.Join(accountFolder, "Video"));
            File.Create(Path.Join(accountFolder, "State.ini")).Dispose();

            return null;
        }
    }
}
