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
        const string KEY = "fund_track!Volunteer..Lv-242.Net";

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
