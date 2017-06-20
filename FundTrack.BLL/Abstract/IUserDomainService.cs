using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;
using System.Security.Claims;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// interface for authorization user
    /// </summary>
    public interface IUserDomainService
    {
        /// <summary>
        /// Login in the site by user in parameter
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return claims which be used in authorize user</returns>
        /// 
        ClaimsIdentity RegisterUserClaim(AuthorizeViewModel user);
    }
}
