using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for user responses
    /// </summary>
    public interface IUserResponseService
    {
        /// <summary>
        /// Gets the user response on requests.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserResponsesOnRequestsViewModel> GetUserResponsesOnRequests(int organizationId);
    }
}
