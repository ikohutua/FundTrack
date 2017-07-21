using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var responses = this._unitOfWork.UserResponseRepository
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
            return responses;
        }

        /// <summary>
        /// Changes the user response status.
        /// </summary>
        /// <param name="changeStatusModel">The new status.</param>
        /// <returns></returns>
        public UserResponsesOnRequestsViewModel ChangeUserResponseStatus(UserResponseChangeStatusViewModel changeStatusModel)
        {
            var userResponse = this._unitOfWork.UserResponseRepository.Get(changeStatusModel.Id);
            userResponse.StatusId = changeStatusModel.Id;
            this._unitOfWork.UserResponseRepository.Update(userResponse);
            this._unitOfWork.SaveChanges();
            return new UserResponsesOnRequestsViewModel
            {
                Id = userResponse.Id,
                RequestedItemName = userResponse.RequestedItem.Name,
                StatusName = userResponse.Status.StatusName,
                UserLogin = userResponse.User != null ? userResponse.User.Login : string.Empty,
                UserEmail = userResponse.User != null ? userResponse.User.Email : string.Empty,
                CreateDate = DateTime.Now,
                OrganizationId = userResponse.RequestedItem.OrganizationId,
                Description = userResponse.Description
            };
        }

        /// <summary>
        /// Gets the user resonse with new status.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public int GetUserResponseWithNewStatus(int organizationId)
        {
            var responses = this._unitOfWork.UserResponseRepository
                                            .ReadAsQueryable()
                                            .Where(u => u.RequestedItem.OrganizationId == organizationId)
                                            .Count();
            return responses;
        }
    }
}
