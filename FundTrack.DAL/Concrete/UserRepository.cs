using FundTrack.DAL.Abstract;
using FundTrack.DAL.Concrete;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Repositories
{
    /// <summary>
    /// Class for CRUD operation with entity - user
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRepository{FundTrack.DAL.Entities.User}" />
    public sealed class UserRepository : IUserResporitory
    {        
        private readonly FundTrackContext context;
      
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRepository(FundTrackContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all users in database
        /// </summary>
        /// <returns>
        /// Collection all entries
        /// </returns>
        public IEnumerable<User> Read()
        {
            return context.Users;
        }

        /// <summary>
        /// Get user by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        /// <!-- Badly formed XML comment ignored for member "M:FundTrack.DAL.Abstract.IRepository`1.Get(System.Int32)" -->
        public User Get(int id)
        {
            return context.Users.Find(id);
        }

        /// <summary>
        /// Create new user in database
        /// </summary>
        /// <param name="user">user entity</param>
        /// <returns>Created user</returns>
        public User Create(User user)
        {
            var insertedUser = context.Users.Add(user);
            return insertedUser.Entity;
        }

        /// <summary>
        /// Update existing user in database
        /// </summary>
        /// <param name="item"></param>
        /// <!-- Badly formed XML comment ignored for member "M:FundTrack.DAL.Abstract.IRepository`1.Update(`0)" -->
        public User Update(User item)
        {
            context.Users.Update(item);
            return item;
        }

        /// <summary>
        /// Deletes user from data base
        /// </summary>
        /// <param name="id">Recives id of entry</param>
        public void Delete(int id)
        {
            context.Users.Remove(Get(id));
        }

        /// <summary>
        /// Chek if user existed
        /// </summary>
        /// <param name="email">Login, email</param>
        /// <param name="login"></param>
        /// <returns>Existed user</returns>
        public bool isUserExisted(string email, string login)
        {
            bool isExsitedUser = this.context.Users.Any(u => u.Email.ToLower() == email.ToLower() ||
                                   u.Login.ToLower() == login.ToLower());

            return isExsitedUser;           
        }

        /// <summary>
        /// Gets the user with this login and password.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="rawPassword">The raw password.</param>
        /// <returns></returns>
        public User GetUser(string login, string hashedPassword)
        {
            return this.context.Users
                               .FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper()
                               && u.Password == hashedPassword);
        }

        /// <summary>
        /// Gets the user with this login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        public User GetUser(string login)
        {
            return this.context.Users
                               .FirstOrDefault(a => a.Login.ToUpper() == login.ToUpper());
        }

        /// <summary>
        /// Gets the facebook user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public User GetFacebookUser(string email)
        {
            return this.context.Users
                               .FirstOrDefault(a => a.Email == email);
        }

        /// <summary>
        /// Checks if user has reset password link
        /// </summary>
        /// <param name="user">user to check</param>
        /// <returns>User reset link status</returns>
        public bool HasUserResetLink(User user)
        {
            return context.Users.Include(u => u.PasswordReset)
                                .Any(u => u.PasswordReset != null && u.PasswordReset.UserID == user.Id);
        }

        /// <summary>
        /// Gets All Users With Ban Status
        /// </summary>
        /// <returns>All Users with ban status</returns>
        public IEnumerable<User> GetAllUsersWithBanStatus()
        {
            return context.Users.Include(u => u.BannedUser);
        }

        /// <summary>
        /// Gets Users with their ban status
        /// </summary>
        /// <returns>Users with ban status</returns>
        public IEnumerable<User> GetUsersWithBanStatus()
        {
            return context.Users.Include(u => u.BannedUser)
                                .Include(u => u.Membership)
                                .Include(u => u.Membership.Role)
                                .Where(u => u.Membership.Role.Name != UserRoles.SuperAdmin);

        }
        
        /// <summary>
        /// Gets user by guid
        /// </summary>
        /// <param name="guid">guid to get user</param>
        /// <returns>User with specifice guid</returns>
        public User GetUserByGuid(string guid)
        {
            return context.Users.Include(u => u.PasswordReset)
                                .FirstOrDefault(u => u.PasswordReset != null && u.PasswordReset.GUID == guid);
        }

        /// <summary>
        /// Unbans user with concrete id
        /// </summary>
        /// <param name="user">User to Ban</param>
        public void UnbanUser(int id)
        {
            var bannedUser = context.BannedUsers.FirstOrDefault(u => u.Id == id);

            context.Remove(bannedUser);
        }

        /// <summary>
        /// Bans user
        /// </summary>
        /// <param name="user">User to ban</param>
        /// <returns>Banned User</returns>
        public void BanUser(BannedUser user)
        {
            context.BannedUsers.Add(user);           
        }

        /// <summary>
        /// Addes new Password reset request
        /// </summary>
        /// <param name="passwordReset">Password reset request</param>
        public void AddUserRecoveryLink(PasswordReset passwordReset)
        {
            context.PasswordResets.Add(passwordReset);
        }

        /// <summary>
        /// Removes Recovery link
        /// </summary>
        /// <param name="id">Id of user</param>
        public void RemoveUserRecoveryLink(int id)
        {
            context.PasswordResets.Remove(context.PasswordResets.FirstOrDefault(u => u.UserID == id));
        }
    }
}
