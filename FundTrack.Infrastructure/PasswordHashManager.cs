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
        public static string GetPasswordHash(string salt, string password)
        {
            var saltBytes = System.Text.Encoding.ASCII.GetBytes(salt);
            using (var rfc2898DeriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 1000))
            {
                return System.Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(128));
            }
        }
    }
}
