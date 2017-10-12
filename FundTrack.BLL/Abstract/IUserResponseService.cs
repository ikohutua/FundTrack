using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;

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

        /// <summary>
        /// Gets Initial data for pagination
        /// </summary>
        /// <returns>Event Initial data</returns>
        int GetRequestedItemPaginationData(int organizationId);

        /// <summary>
        /// Gets  RequestedItem per page.
        /// </summary>
        /// <returns>Collection of UserResponsesOnRequestsViewModel</returns>
        IEnumerable<UserResponsesOnRequestsViewModel> GetRequestedItemToShowPerPage(int organizationId, int itemsPerPage, int currentPage);

        /// <summary>
        /// delete user response
        /// </summary>
        /// <param name="responseId"></param>
        void DeleteUserResponse(int responseId);

    }
}
