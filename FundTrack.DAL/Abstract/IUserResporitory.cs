using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    public interface IUserResporitory : IRepository<User>
    {
        bool isUserExisted(string email, string login);
    }
}
