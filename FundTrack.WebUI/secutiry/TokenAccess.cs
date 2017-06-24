using FundTrack.Infrastructure.ViewModel;
using FundTrack.WebUI.token;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FundTrack.WebUI.secutiry
{
    /// <summary>
    /// Class for creating token for authorize user
    /// </summary>
    public class TokenAccess
    {
        /// <summary>
        /// Creates the token for user access which contains setting from AuthOptions class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns>Token</returns>
        public string CreateTokenAccess(UserInfoViewModel user)
        {
            var identity = CreateClaim(user);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Creates the claim for any user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Claims identity</returns>
        private ClaimsIdentity CreateClaim(UserInfoViewModel user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.login)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                                               "Token",
                                                               ClaimsIdentity.DefaultNameClaimType,
                                                               ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
