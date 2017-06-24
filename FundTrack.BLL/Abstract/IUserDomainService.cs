using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
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
        
    }
}
