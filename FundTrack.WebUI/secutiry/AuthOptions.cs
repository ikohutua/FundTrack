using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FundTrack.WebUI.token
{
    /// <summary>
    /// Options for creating token
    /// </summary>
    public class AuthOptions
    {     
        /// <summary>
        /// The key for encryption
        /// </summary>
        const string KEY = "mysupersecret_secretkey!123";
        /// <summary>
        /// The lifetime token
        /// </summary>
        public const int LIFETIME = 10;

        /// <summary>
        /// Gets the symmetric security key.
        /// </summary>
        /// <returns>Symetric Key</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
