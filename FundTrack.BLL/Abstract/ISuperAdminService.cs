using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    public interface ISuperAdminService
    {
        /// <summary>
        /// Gets Users per page
        /// </summary>
        /// <param name="currentPage">Current page on grid</param>
        /// <returns>Users for specifice page</returns>
        IEnumerable<PagingItemsViewModel> GetUsersPerPage(int currentPage,int pageSize);

        /// <summary>
        /// Gets Organizations per page
        /// </summary>
        /// <param name="currentPage">Current page on grid</param>
        /// <returns>Organizations for specifice page</returns>
        IEnumerable<PagingItemsViewModel> GetOrganizationsPerPage(int currentPage,int pageSize);

        /// <summary>
        /// Gets Initial data for user pagination
        /// </summary>
        /// <returns>Pagination Initial data</returns>
        PaginationInitViewModel GetUsersPaginationData();

        /// <summary>
        /// Gets Initial data for organization pagination
        /// </summary>
        /// <returns>Pagination Initial data</returns>
        PaginationInitViewModel GetOrganizationPaginationData();

        /// <summary>
        /// Changes Ban status of User
        /// </summary>
        /// <param name="banStatusViewModel">Item to change ban status</param>
        void ChangeUserBanStatus(BanStatusViewModel banStatusViewModel);

        /// <summary>
        /// Changes Ban status of Organization
        /// </summary>
        /// <param name="banStatusViewModel">Item to change ban status</param>
        void ChangeOrganizationBanStatus(BanStatusViewModel banStatusViewModel);
    }
}
