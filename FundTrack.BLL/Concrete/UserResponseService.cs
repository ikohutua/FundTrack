using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Service for user response
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IUserResponseService" />
    public class UserResponseService : IUserResponseService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserResponseService(IUnitOfWork unitOfWorkParam)
        {
            _unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Gets the user response on requests.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserResponsesOnRequestsViewModel> GetUserResponsesOnRequests(int organizationId)
        {
            try
            {
                return this._unitOfWork.UserResponseRepository
                                                .ReadAsQueryable()
                                                .Select(u => new UserResponsesOnRequestsViewModel
                                                {
                                                    Id = u.Id,
                                                    RequestedItemName = u.RequestedItem.Name,
                                                    StatusName = u.Status.StatusName,
                                                    UserLogin = u.User.Login,
                                                    UserEmail = u.User.Email,
                                                    CreateDate = DateTime.Now,
                                                    OrganizationId = u.RequestedItem.OrganizationId,
                                                    Description = u.Description
                                                })
                                                .Where(u => u.OrganizationId == organizationId);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Changes the user response status.
        /// </summary>
        /// <param name="changeStatusModel">The new status.</param>
        /// <returns></returns>
        public UserResponsesOnRequestsViewModel ChangeUserResponseStatus(UserResponseChangeStatusViewModel changeStatusModel)
        {
            try
            {
                var userResponse = this._unitOfWork.UserResponseRepository.Get(changeStatusModel.Id);
                userResponse.StatusId = changeStatusModel.NewStatusId;
                this._unitOfWork.UserResponseRepository.Update(userResponse);
                this._unitOfWork.SaveChanges();
                var updateUserResponse = this._unitOfWork.UserResponseRepository.Get(changeStatusModel.Id);
                return new UserResponsesOnRequestsViewModel
                {
                    Id = userResponse.Id,
                    RequestedItemName = updateUserResponse.RequestedItem.Name,
                    StatusName = updateUserResponse.Status.StatusName,
                    UserLogin = updateUserResponse.User != null ? updateUserResponse.User.Login : string.Empty,
                    UserEmail = updateUserResponse.User != null ? updateUserResponse.User.Email : string.Empty,
                    CreateDate = DateTime.Now,
                    OrganizationId = updateUserResponse.RequestedItem.OrganizationId,
                    Description = updateUserResponse.Description
                };
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the user resonse with new status.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public int GetUserResponseWithNewStatus(int organizationId)
        {
            try
            {
                return this._unitOfWork.UserResponseRepository
                                       .ReadAsQueryable()
                                       .Where(u => u.RequestedItem.OrganizationId == organizationId && u.Status.StatusName == StuffStatus.New)
                                       .Count();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets Initial data for pagination
        /// </summary>
        /// <returns>Event Initial data</returns>
        public int GetRequestedItemPaginationData(int organizationId)
        {
            try
            {
                return _unitOfWork.UserResponseRepository.ReadAsQueryable()
                                                         .Where(u => u.RequestedItem.OrganizationId == organizationId)
                                                         .Count();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets  RequestedItem per page.
        /// </summary>
        /// <returns>Collection of UserResponsesOnRequestsViewModel</returns>
        public IEnumerable<UserResponsesOnRequestsViewModel> GetRequestedItemToShowPerPage(int organizationId, int itemsPerPage, int currentPage)
        {
            try
            {
                return this._unitOfWork.UserResponseRepository
                                       .ReadForPagination(organizationId, itemsPerPage, currentPage)
                                       .Select(u => new UserResponsesOnRequestsViewModel
                                       {
                                           Id = u.Id,
                                           RequestedItemName = u.RequestedItem.Name,
                                           StatusName = u.Status.StatusName,
                                           UserLogin = u.User.Login,
                                           UserEmail = u.User.Email,
                                           CreateDate = DateTime.Now,
                                           OrganizationId = u.RequestedItem.OrganizationId,
                                           Description = u.Description
                                       });
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// delete user respone
        /// </summary>
        /// <param name="responseId"></param>
        public void DeleteUserResponse(int responseId)
        {
            try
            {
                this._unitOfWork.UserResponseRepository.Delete(responseId);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
    }
}
