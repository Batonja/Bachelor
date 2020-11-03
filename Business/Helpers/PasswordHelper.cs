using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Helpers
{
    public static class PasswordHelper
    {
        public const int SALT_SIZE = 24; // size in bytes
        public const int HASH_SIZE = 24; // size in bytes
        public const int ITERATIONS = 100000; // number of pbkdf2 iterations

        public static string[] EncryptPassword(string password)
        {
            if (password == null)
            {
                password = "null";
            }
            // Generate a salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE];
            provider.GetBytes(salt);
            string encodedSalt;
            string encodedKey;

            using (var derivedBytes = new Rfc2898DeriveBytes(password, salt, ITERATIONS))
            {

                encodedSalt = Convert.ToBase64String(salt);
                encodedKey = Convert.ToBase64String(derivedBytes.GetBytes(HASH_SIZE));
            }
            string[] returnString = new string[2];
            returnString[0] = encodedSalt;
            returnString[1] = encodedKey;
            return returnString;



        }

        public static string GetMeAString(string encryptedPassword)
        {

            StringBuilder literal = new StringBuilder(encryptedPassword.Length + 2);

            foreach (var c in encryptedPassword)
            {
                switch (c)
                {
                    case '\'': literal.Append(@"\'"); break;
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        // ASCII printable character
                        if (c >= 0x20 && c <= 0x7e)
                        {
                            literal.Append(c);
                            // As UTF16 escaped character
                        }
                        else
                        {
                            literal.Append(@"\u");
                            literal.Append(((int)c).ToString("x4"));
                        }
                        break;
                }
            }

            return literal.ToString();

        }

        public static bool Autheticate(string encryptedPassword, string encryptedSalt, string passwordToCheck)
        {
            byte[] salt = Convert.FromBase64String(encryptedSalt);
            byte[] key = Convert.FromBase64String(encryptedPassword);

            using (var derivedBytes = new Rfc2898DeriveBytes(passwordToCheck, salt, ITERATIONS))
            {
                byte[] testedValue = derivedBytes.GetBytes(HASH_SIZE);

                return !testedValue.SequenceEqual(key) ? false : true;
            }

        }


    }
}
