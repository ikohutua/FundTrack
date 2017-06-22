using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.WebUI.secutiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    ///This is a controller to get access to user in db and authorize their
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
        /// Authorize user in system , return the type which contain UserInfoViewModel
        /// </summary>
        [HttpPost("LogIn")]
        public string LogIn([FromBody]AuthorizeViewModel user)
        {
            var authorizeToken = new TokenAccess();
            try
            {

                var authorizationType = this._getAuthorizationType(user.Login, user.Password);

                return JsonConvert.SerializeObject(authorizationType, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            catch (Exception ex)
            {
                var authorizationType = new AuthorizationType
                {
                    access_token = "",
                    errorMessage = ex.Message
                };
                return JsonConvert.SerializeObject(authorizationType, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
        }

        /// <summary>
        /// Check is user authorized
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Login authorize user</returns>
        [HttpPost("[action]")]
        [Authorize]
        public JsonResult Name([FromBody] string login)
        {
            return Json(HttpContext.User.Identity.Name);
        }

        /// <summary>
        /// Register user 
        /// </summary>
        /// <param name="registrationViewModel">RegistrationViewModel</param>
        /// <returns>Action result</returns>
        [HttpPost("[action]")]
        public string Register([FromBody]RegistrationViewModel registrationViewModel)
        {               
            try
            {
                var user = _userDomainService.CreateUser(registrationViewModel);
                
                var authorizationType = this._getAuthorizationType(registrationViewModel.Login,
                                                                   registrationViewModel.Password);
                
                return JsonConvert.SerializeObject(authorizationType, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            catch (Exception ex)
            {
                var authorizationType = new AuthorizationType
                {
                    access_token = "",
                    errorMessage = ex.Message
                };
                return JsonConvert.SerializeObject(authorizationType, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
        }

        /// <summary>
        /// Method for generate token
        /// </summary>
        /// <param name="userLogin">User login </param>
        /// <param name="rawPassword">User raw password</param>
        /// <returns>Authorization type model</returns>
        private AuthorizationType _getAuthorizationType(string userLogin, string rawPassword)
        {
            var authorizeToken = new TokenAccess();

            var userInfoModel = _userDomainService.GetUserInfoViewModel(userLogin, rawPassword);
            var encodedJwt = authorizeToken.CreateTokenAccess(userInfoModel);
            var authorizationType = new AuthorizationType
            {
                access_token = encodedJwt,
                login = userInfoModel.userLogin,
                id = userInfoModel.userId,
                firstName = userInfoModel.userFirstName,
                lastName = userInfoModel.userLastName,
                email = userInfoModel.userEmail,
                address = userInfoModel.userAddress,
                photoUrl = userInfoModel.userPhotoUrl
            };

            return authorizationType;
        }
    }
}