using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Abstract
{
    public interface IUserDomainService
    {
        User CreateUser(User user);
    }
}
