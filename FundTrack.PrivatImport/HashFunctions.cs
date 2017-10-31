using System.Security.Cryptography;
using System.Text;

namespace FundTrack.PrivatImport
{
    public static class HashFunctions
    {
        public static string CalculateMd5Hash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(inputBytes);
                return HexStringFromBytes(hashBytes);
            }
        }

        public static string CalculateSha1Hash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(inputBytes);
                return HexStringFromBytes(hashBytes);
            }
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
