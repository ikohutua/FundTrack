using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller for super admin actions
    /// </summary>
    [Route("api/[controller]")]
    public class SuperAdminController : Controller
    {        
        // instance of userService
        private readonly ISuperAdminService _superAdminService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperAdminController"/> class.
        /// </summary>
        /// <param name="serviceParam">The service parameter.</param>
        public SuperAdminController(ISuperAdminService superAdminServiceParam)
        {
            _superAdminService = superAdminServiceParam;
        }

        /// <summary>
        /// Gets Users Data for pagination
        /// </summary>
        /// <returns>Users pagination data</returns>
        [HttpGet("[action]")]
        public PaginationInitViewModel GetUsersPaginationData()
        {
            return _superAdminService.GetUsersPaginationData();
        }

        /// <summary>
        /// Gets Users for page
        /// </summary>
        /// <param name="currentPage">Current page on grid</param>
        /// <returns>Users for specifice page</returns>
        [HttpGet("[action]/{currentPage}/{itemsPerPage}")]
        public IEnumerable<PagingItemsViewModel> GetUsersPerPage(int currentPage, int itemsPerPage)
        {
            return _superAdminService.GetUsersPerPage(currentPage, itemsPerPage);
        }

        /// <summary>
        /// Gets Organizations Data for pagination
        /// </summary>
        /// <returns>Organizations pagination data</returns>
        [HttpGet("[action]")]
        public PaginationInitViewModel GetOrganizationsPaginationData()
        {
            return _superAdminService.GetOrganizationPaginationData();
        }

        /// <summary>
        /// Gets Organizations for page
        /// </summary>
        /// <param name="currentPage">Current page on grid</param>
        /// <returns>Organizations for specifice page</returns>
        [HttpGet("[action]/{currentPage}/{itemsPerPage}")]
        public IEnumerable<PagingItemsViewModel> GetOrganizationsPerPage(int currentPage, int itemsPerPage)
        {
            return _superAdminService.GetOrganizationsPerPage(currentPage, itemsPerPage);
        }

        /// <summary>
        /// Changes ban status of Organization
        /// </summary>
        /// <param name="banStatusViewModel">Item to change ban status</param>
        [HttpPost("[action]")]
        public void ChangeOrganizationBanStatus([FromBody]BanStatusViewModel banStatusViewModel)
        {
            _superAdminService.ChangeOrganizationBanStatus(banStatusViewModel);
        }

        /// <summary>
        /// Changes ban status of User
        /// </summary>
        /// <param name="banStatusViewModel">Item to change ban status</param>
        [HttpPost("[action]")]
        public void ChangeUserBanStatus([FromBody]BanStatusViewModel banStatusViewModel)
        {
            _superAdminService.ChangeUserBanStatus(banStatusViewModel);
        }
    }
}
