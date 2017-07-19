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
                                               StatusName = "New",
                                               UserLogin = u.User.Login,
                                               UserEmail = u.User.Email,
                                               CreateDate = DateTime.Now,
                                               OrganizationId = u.RequestedItem.OrganizationId,
                                               Description = u.Description
                                           })
                                            .Where(u => u.OrganizationId == organizationId);
            return responses;
        }
    }
}
