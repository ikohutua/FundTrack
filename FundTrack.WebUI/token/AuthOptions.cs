using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.WebUI.token
{
    public class AuthOptions
    {
        /// <summary>
        /// token publisher
        /// </summary>
        public const string ISSUER = "http://localhost:51116";
        /// <summary>
        /// token consumer
        /// </summary>
        public const string AUDIENCE = "http://localhost:51116";     
        /// <summary>
        /// The key for encryption
        /// </summary>
        const string KEY = "mysupersecret_secretkey!123";
        /// <summary>
        /// The lifetime token
        /// </summary>
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
