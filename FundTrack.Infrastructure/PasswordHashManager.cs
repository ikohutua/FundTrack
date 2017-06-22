using System.Security.Cryptography;
using System.Text;

namespace FundTrack.Infrastructure
{
    /// <summary>
    /// Class for hashing password
    /// </summary>
    public static class PasswordHashManager
    {
        /// <summary>
        /// Hashes user password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Hash string</returns>
        public static string GetPasswordHash(string password)
        {
            // use some key for hashing
            const string hashKey = "someSecretKey";
            // create hash class. 
            HMACSHA512 sha1 = new HMACSHA512();
            // set key 
            sha1.Key = Encoding.ASCII.GetBytes(hashKey);

            // get bytes from password
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            // compute hash value
            byte[] sha1data = sha1.ComputeHash(passwordBytes);

            // get string from bytes
            ASCIIEncoding ascii = new ASCIIEncoding();
            var hashedPassword = ascii.GetString(sha1data);

            return hashedPassword;
        }
    }
}
