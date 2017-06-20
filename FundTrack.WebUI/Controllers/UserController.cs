using FundTrack.BLL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.WebUI.token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    ///this is a controller to get access to user in db and authorize their
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserDomainService _userDomainService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userDomainService">The user domain service.</param>
        public UserController(IUserDomainService userDomainService)
        {
            this._userDomainService = userDomainService;
        }
        /// <summary>
        /// authorize user in system , create access_token for him
        /// </summary>
        /// 
        [HttpPost("LogIn")]
        public string Post([FromBody]AuthorizeViewModel user)
        {
            try
            {
                var identity = _userDomainService.RegisterUserClaim(user);
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var AuthorizationType = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };
                Response.StatusCode = 200;
                return JsonConvert.SerializeObject(AuthorizationType, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// check is user authorized
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [Authorize]
        public JsonResult Name([FromBody] string login)
        {
            return Json(HttpContext.User.Identity.Name);
        }      
    }
}