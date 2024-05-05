using System.Security.Cryptography;
using System.Text;

namespace Electro_goods_API.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPasword(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
