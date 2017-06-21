using FundTrack.DAL.Abstract;
using FundTrack.DAL.Concrete;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Repositories
{
    /// <summary>
    /// Class for CRUD operation with entity - user
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRepository{FundTrack.DAL.Entities.User}" />
    public sealed class UserRepository : IRepository<User>
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
        /// <param name="item"></param>
        /// <returns>New user</returns>
        /// <!-- Badly formed XML comment ignored for member "M:FundTrack.DAL.Abstract.IRepository`1.Create(`0)" -->
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
        public void Update(User item)
        {
            context.Users.Update(item);
        }

        /// <summary>
        /// Deletes user from data base
        /// </summary>
        /// <param name="id">Recives id of entry</param>
        public void Delete(int id)
        {
            context.Users.Remove(Get(id));
        }
    }
}
