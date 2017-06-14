

using FundTrack.Infrastructure.Models;

namespace FundTrack.DAL.Abstract
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool IsUserExists(User user);
    }
}
