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

        /// <summary>
        /// Changes the user response status.
        /// </summary>
        /// <param name="newStatus">The new status.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <returns></returns>
        UserResponsesOnRequestsViewModel ChangeUserResponseStatus(UserResponseChangeStatusViewModel newStatus);

        /// <summary>
        /// Gets the user resonse with new status.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        int GetUserResponseWithNewStatus(int organizationId);
    }
}
