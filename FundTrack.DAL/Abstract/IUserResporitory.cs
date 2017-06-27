using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface IUserResporitory : IRepository<User>
    {
        bool isUserExisted(string email, string login);

        /// <summary>
        /// Gets Users with their ban status
        /// </summary>
        /// <returns>Users with ban status</returns>
        IEnumerable<User> GetUsersWithBanStatus();

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
    }
}
