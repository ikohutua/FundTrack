using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.ResetPassword;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for authorization user
    /// </summary>
    public interface IUserDomainService
    {

        /// <summary>
        /// Call method GetUser from db and map to type UserInfoViewModel
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Model which contain information about user</returns>
        /// <exception cref="System.Exception">Login or password are not correct</exception>
        UserInfoViewModel GetUserInfoViewModel(string login, string rawPassword);
        UserInfoViewModel GetUserInfoViewModel(string login);
        UserInfoViewModel UpdateUser(UserInfoViewModel userModel);
        UserInfoViewModel ChangePassword(ChangePasswordViewModel changePasswordViewModel);

        /// <summary>
        /// Gets organization Id with bann status
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Organization Id with ban status</returns>
        OrganizationIdViewModel GetOrganizationId(string login);

        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>List of users</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Creates user in database
        /// </summary>
        /// <param name="registrationViewModel">RegistrationViewModel</param>
        /// <returns>Registration view model</returns>
        RegistrationViewModel CreateUser(RegistrationViewModel registrationViewModel);

        /// <summary>
        /// Send Email with recovery password link
        /// </summary>
        /// <param name="currentHost">current host</param>
        /// <param name="email">email to send address</param>
        void SendPasswordRecoveryEmail(string currentHost, string email);

        /// <summary>
        /// Resets user password
        /// </summary>
        /// <param name="passwordReset">view model of PasswordResetViewModel</param>
        void ResetPassword(PasswordResetViewModel passwordReset);

        /// <summary>
        /// Checks if input guid is a valid user guid
        /// </summary>
        /// <param name="guid">Input User guid</param>
        /// <returns>Guid status</returns>
        bool IsValidUserGuid(string guid);

        /// <summary>
        /// Login with facebook.
        /// </summary>
        /// <param name="loginFacebookViewModel">The login facebook view model.</param>
        /// <returns></returns>
        UserInfoViewModel LoginFacebook(LoginFacebookViewModel loginFacebookViewModel);

        /// <summary>
        /// Checks if the user with email exists
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>User email status</returns>
        bool IsValidUserEmail(string email);
    }
}
