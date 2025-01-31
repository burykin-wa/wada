using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helper
{
    internal static class PasswordEncryption
    {
        public static string EncryptPassword(string password)
        {
            HashAlgorithm hashAlg = MD5.Create();
            var hash = hashAlg.ComputeHash(Encoding.Unicode.GetBytes(password));
            return Convert.ToBase64String(hash);                
        }
    }
}
