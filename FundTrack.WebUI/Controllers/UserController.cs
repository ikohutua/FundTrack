using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.ResetPassword;
using FundTrack.WebUI.secutiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    ///This is a controller to get access to user in db and authorize their
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/User")]
    public sealed class UserController : Controller
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
        /// Authorize facebook user in system 
        /// </summary>
        /// <param name="loginFacebookViewModel">The login facebook view model.</param>
        /// <returns></returns>
        [HttpPost("LogInFacebook")]
        public string LogInFacebook([FromBody] LoginFacebookViewModel loginFacebookViewModel)
        {
            try
            {
                var userInfo = this._userDomainService.LoginFacebook(loginFacebookViewModel);
                var userInfoModel = _userDomainService.GetUserInfoViewModel(userInfo.login);
                return JsonConvert.SerializeObject(this._getAuthorizationType(userInfoModel),
                                                       new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            catch (Exception ex)
            {
                return this._getAuthorizationTypeError(ex.Message);
            }
        }

        /// <summary>
        ///  Authorize user in system , return the type which contain UserInfoViewModel
        /// </summary>
        /// <param name="user">user credentials</param>
        /// <returns>authorization type with token and user model</returns>
        [HttpPost("LogIn")]
        public string LogIn([FromBody]LoginViewModel user)
        {
            try
            {
                var userInfoModel = _userDomainService.GetUserInfoViewModel(user.Login, user.Password);
                return JsonConvert.SerializeObject(this._getAuthorizationType(userInfoModel),
                                                   new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            catch (Exception ex)
            {
                return this._getAuthorizationTypeError(ex.Message);
            }
        }

        /// <summary>
        /// Edit user based on the view model received
        /// </summary>
        /// <param name="model">View model received from frontend</param>
        /// <returns>stringified View model</returns>
        [Authorize]
        [HttpPut("editprofile")]
        public JsonResult EditProfile([FromBody] UserInfoViewModel model)
        {
            var updatedUser = this._userDomainService.UpdateUser(model);
            return Json(updatedUser);
        }

        /// <summary>
        /// Changes password of specified User entity by his/her login
        /// </summary>
        /// <param name="changePasswordViewModel"></param>
        /// <returns>returns change password view model, that is empty if change password succeded</returns>
        [Authorize]
        [HttpPost("changepassword")]
        public JsonResult ChangePassword([FromBody]ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                var updatedUserModel = this._userDomainService.ChangePassword(changePasswordViewModel);
                return Json(new ChangePasswordViewModel());
            }
            catch (Exception ex)
            {
                var changePasswordReturnModel = new ChangePasswordViewModel();
                {
                    changePasswordViewModel.errorMessage = ex.Message;
                };
                return Json(changePasswordViewModel);
            }
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
                if (ModelState.IsValid)
                {
                    var user = _userDomainService.CreateUser(registrationViewModel);
                    var userInfoModel = _userDomainService.GetUserInfoViewModel(registrationViewModel.Login, registrationViewModel.Password);
                    return JsonConvert.SerializeObject(this._getAuthorizationType(userInfoModel),
                                                       new JsonSerializerSettings { Formatting = Formatting.Indented });
                }
                else
                {
                    List<ValidationViewModel> validationSummary = new List<ValidationViewModel>();

                    foreach (var field in ModelState.Keys)
                    {
                        var erorMessages = ModelState[field].Errors.Select(a => a.ErrorMessage);
                        validationSummary.Add(new ValidationViewModel
                        {
                            ErrorsMessages = erorMessages.ToList(),
                            FieldName = field
                        });
                    }

                    return this._getAuthorizationTypeError(validationSummary: validationSummary);
                }
            }
            catch (Exception ex)
            {
                return this._getAuthorizationTypeError(ex.Message);
            }
        }

        /// <summary>
        /// Checks User Email status
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>User Email status</returns>
        [HttpPost("[action]")]
        public JsonResult CheckEmailStatus([FromBody]UserEmailViewModel email)
        {
            if (_userDomainService.IsValidUserEmail(email.Email))
            {
               return Json(string.Empty);
            }

            return Json(ErrorMessages.NoUserWithEmail);
        }

        /// <summary>
        /// Sends Recovery Email for User Password
        /// </summary>
        /// <param name="email">Address to send Email</param>
        /// <returns>Error string if the Email faild to send</returns>
        [HttpPost("[action]")]
        public JsonResult SendRecoveryEmail([FromBody]UserEmailViewModel email)
        {
            try
            {
                _userDomainService.SendPasswordRecoveryEmail(GetUri(), email.Email);

                return Json(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        /// <summary>
        /// Checks if the guid is valid
        /// </summary>
        /// <param name="guid">Guid number</param>
        /// <returns>Error string if the guid is invalid</returns>
        [HttpPost("[action]")]
        public JsonResult CheckGuidStatus([FromBody]GuidViewModel guid)
        {
            if (_userDomainService.IsValidUserGuid(guid.Guid))
            {
                return Json(string.Empty);
            }

            return Json(ErrorMessages.InvalidGuid);
        }

        /// <summary>
        /// Resets password to user
        /// </summary>
        /// <param name="resetViewModel">View Model to reset password</param>
        /// <returns>Error string if the password faild to reset</returns>
        [HttpPost("[action]")]
        public JsonResult ResetUserPassword([FromBody]PasswordResetViewModel resetViewModel)
        {
            try
            {
                _userDomainService.ResetPassword(resetViewModel);

                return Json(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // gets current request uri
        private string GetUri() => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        // gets Authorization Type Errors
        private string _getAuthorizationTypeError(string errorMessage = "", List<ValidationViewModel> validationSummary = null)
        {
            var authorizationType = new AuthorizationType
            {
                access_token = String.Empty,
                errorMessage = errorMessage,
                validationSummary = validationSummary
            };

            return JsonConvert.SerializeObject(authorizationType, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private AuthorizationType _getAuthorizationType(UserInfoViewModel userInfoModel)
        {
            var authorizeToken = new TokenAccess();
            var encodedJwt = authorizeToken.CreateTokenAccess(userInfoModel);
            var authorizationType = new AuthorizationType
            {
                userModel = userInfoModel,
                access_token = encodedJwt,
            };
            return authorizationType;
        }
    }
}