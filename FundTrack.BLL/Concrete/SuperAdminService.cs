using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Service for Super Admin Actions
    /// </summary>
    public sealed class SuperAdminService : BaseService, ISuperAdminService
    {
        // sets page size
        private const int PageSize = 4;
       
        // unit of work instance
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperAdminService"/> class.
        /// </summary>
        /// <param name="unitOfWorkParam">The unit of work parameter</param>
        public SuperAdminService(IUnitOfWork unitOfWorkParam) : base()
        {
            _unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Gets Users per page
        /// </summary>
        /// <param name="currentPage">Current page on grid</param>
        /// <returns>Users for specifice page</returns>
        public IEnumerable<PagingItemsViewModel> GetUsersPerPage(int currentPage, int pageSize)
        {           
            return GetPageItems(_unitOfWork.UsersRepository.GetUsersWithBanStatus(),
                                pageSize, currentPage)                               
                                .Select(i => new PagingItemsViewModel
                                {
                                    Id = i.Id,
                                    Title = i.Login,
                                    IsBanned = i.BannedUser == null ? false : true,
                                    BannDescription = i.BannedUser == null ? string.Empty : i.BannedUser.Description
                                });
        }

        /// <summary>
        /// Gets Organizations per page
        /// </summary>
        /// <param name="currentPage">Current page on grid</param>
        /// <returns>Organizations for specifice page</returns>
        public IEnumerable<PagingItemsViewModel> GetOrganizationsPerPage(int currentPage,int pageSize)
        {
            return GetPageItems(_unitOfWork.OrganizationRepository.GetOrganizationsWithBanStatus(),
                                pageSize, currentPage)
                                .Select(i => new PagingItemsViewModel
                                {
                                    Id = i.Id,
                                    Title = i.Name,
                                    IsBanned = i.BannedOrganization == null ? false : true,
                                    BannDescription = i.BannedOrganization == null ? string.Empty : i.BannedOrganization.Description
                                });
        }

        /// <summary>
        /// Gets Initial data for user pagination
        /// </summary>
        /// <returns>Pagination Initial data</returns>
        public PaginationInitViewModel GetUsersPaginationData()
        {
            return GetPaginationInitData(_unitOfWork.UsersRepository.GetUsersWithBanStatus(), PageSize);
        }

        /// <summary>
        /// Gets Initial data for organization pagination
        /// </summary>
        /// <returns>Pagination Initial data</returns>
        public PaginationInitViewModel GetOrganizationPaginationData()
        {
            return GetPaginationInitData(_unitOfWork.OrganizationRepository.Read(), PageSize);
        }

        /// <summary>
        /// Changes Ban status of User
        /// </summary>
        /// <param name="banStatusViewModel">Item to change ban status</param>
        public void ChangeUserBanStatus(BanStatusViewModel banStatusViewModel)
        {
            var bannedUser = _unitOfWork.UsersRepository.GetUsersWithBanStatus()
                                                        .FirstOrDefault(u => u.Id == banStatusViewModel.Id)
                                                        .BannedUser;

            if (bannedUser == null)
            {
                _unitOfWork.UsersRepository.BanUser(new BannedUser
                {
                    UserId = banStatusViewModel.Id,
                    Description = banStatusViewModel.BanDescription
                });
            }
            else
            {
                _unitOfWork.UsersRepository.UnbanUser(bannedUser.Id);
            }

            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Changes Ban status of Organization
        /// </summary>
        /// <param name="banStatusViewModel">Item to change ban status</param>
        public void ChangeOrganizationBanStatus(BanStatusViewModel banStatusViewModel)
        {
            var bannedOrg = _unitOfWork.OrganizationRepository.GetOrganizationsWithBanStatus()
                                                              .FirstOrDefault(o => o.Id == banStatusViewModel.Id)
                                                              .BannedOrganization;
            if (bannedOrg == null)
            {
                _unitOfWork.OrganizationRepository.BanOrganization(new BannedOrganization
                {
                    OrganizationId = banStatusViewModel.Id,
                    Description = banStatusViewModel.BanDescription
                });
            }
            else
            {
                _unitOfWork.OrganizationRepository.UnBanOrganization(bannedOrg.Id);
            }

            _unitOfWork.SaveChanges();
        }
    }
}
