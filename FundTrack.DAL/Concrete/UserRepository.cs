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
            context.Users.Add(user);
            return user;
        }

        /// <summary>
        /// Update existing user in database
        /// </summary>
        /// <param name="item"></param>
        /// <!-- Badly formed XML comment ignored for member "M:FundTrack.DAL.Abstract.IRepository`1.Update(`0)" -->
        public User Update(User item)
        {
            context.Users.Update(item);
            context.SaveChanges();
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
        /// Gets Users with their ban status
        /// </summary>
        /// <returns>Users with ban status</returns>
        public IEnumerable<User> GetUsersWithBanStatus()
        {
            return context.Users.Include(u => u.BannedUser);
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
    }
}
