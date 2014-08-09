using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PropertyManager.Cryptography
{
    public sealed class PasswordCrypt
    {
        public static string Encrypt(string pswd, string key = "{d588170c-1620-41bc-91b8-5e4b4cb1bca3}")
        {
            byte[] bytePswd = Encoding.UTF8.GetBytes(pswd);

            byte[] keyBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes("{653dcb69-a157-4f9b-bed9-8fb9a0957d07}")).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.UTF8.GetBytes("{e88aff89-59aa-4000-bfcd-0c9259c90189}"));

            byte[] byteEncPswd;
            using (var memStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytePswd, 0, bytePswd.Length);
                    cryptoStream.FlushFinalBlock();
                    byteEncPswd = memStream.ToArray();
                    cryptoStream.Close();
                }
                memStream.Close();
            }

            return Convert.ToBase64String(byteEncPswd);
        }

        public static string Decrypt(string encPswd, string key = "{d588170c-1620-41bc-91b8-5e4b4cb1bca3}")
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encPswd);
            byte[] keyBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes("{653dcb69-a157-4f9b-bed9-8fb9a0957d07}")).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.UTF8.GetBytes("{e88aff89-59aa-4000-bfcd-0c9259c90189}"));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.Close();
            memoryStream.Close();
            cryptoStream.Dispose();
            memoryStream.Dispose();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}
