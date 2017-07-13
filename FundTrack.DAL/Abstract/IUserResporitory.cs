using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface IUserResporitory : IRepository<User>
    {
        /// <summary>
        /// Cheks if user existed
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="login">User login</param>
        /// <returns>Existed user</returns>
        bool isUserExisted(string email, string login);

        /// <summary>
        /// Gets Users with their ban status
        /// </summary>
        /// <returns>Users with ban status</returns>
        IEnumerable<User> GetUsersWithBanStatus();

        /// <summary>
        /// Gets All Users With Ban Status
        /// </summary>
        /// <returns>All Users with ban status</returns>
        IEnumerable<User> GetAllUsersWithBanStatus();

        /// <summary>
        /// Checks if user has reset password link
        /// </summary>
        /// <param name="user">user to check</param>
        /// <returns>User reset link status</returns>
        bool HasUserResetLink(User user);

        /// <summary>
        ///  Unbans user with concrete id
        /// </summary>
        /// <param name="id">Id of User</param>
        void UnbanUser(int id);

        /// <summary>
        /// Bans user
        /// </summary>
        /// <param name="user">User to ban</param>
        /// <returns>Banned User</returns>
        void BanUser(BannedUser user);

        /// <summary>
        /// Addes new Password reset request
        /// </summary>
        /// <param name="passwordReset">Password reset request</param>
        void AddUserRecoveryLink(PasswordReset passwordReset);

        /// <summary>
        /// Removes Recovery link
        /// </summary>
        /// <param name="id">Id of user</param>
        void RemoveUserRecoveryLink(int id);

        /// <summary>
        /// Gets user by guid
        /// </summary>
        /// <param name="guid">guid to get user</param>
        /// <returns>User with specifice guid</returns>
        User GetUserByGuid(string guid);

        /// <summary>
        /// Get the user with this login and password.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="rawPassword">The raw password.</param>
        /// <returns></returns>
        User GetUser(string login, string rawPassword);

        /// <summary>
        /// Gets the user with this login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        User GetUser(string login);

        /// <summary>
        /// Gets the facebook user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        User GetFacebookUser(string email);

        /// <summary>
        /// Gets User entity by id
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User entity</returns>
        User GetUserById(int id);
    }
}
