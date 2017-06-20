using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.DAL.Abstract
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool IsUserExists(User user);
        /// <summary>
        /// return user which have password and login which are entered in login form
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User LogIn(User user);
    }
}
