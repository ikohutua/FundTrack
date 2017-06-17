using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool IsUserExists(User user);
    }
}
