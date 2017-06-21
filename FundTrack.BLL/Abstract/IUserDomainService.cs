using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for authorization user
    /// </summary>
    public interface IUserDomainService
    {
        /// <summary>
        /// Login in the site by user in parameter
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return claims which be used in authorize user</returns>
        UserInfoViewModel GetUserInfoViewModel(AuthorizeViewModel user);
    }
}
