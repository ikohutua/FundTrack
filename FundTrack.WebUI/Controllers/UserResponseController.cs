using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    ///This is a controller for user responses
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/UserResponse")]
    public class UserResponseController : Controller
    {
        private readonly IUserResponseService _userResponseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserResponseController"/> class.
        /// </summary>
        /// <param name="userDomainService">The user response service.</param>
        public UserResponseController(IUserResponseService userResponseService)
        {
            this._userResponseService = userResponseService;
        }

        /// <summary>
        /// Methods for get all user responses with additional information
        /// </summary>
        /// <returns>List of user responses</returns>
        [HttpGet("GetUserResponse/{organizationId}")]
        public IEnumerable<UserResponsesOnRequestsViewModel> GetUserResponsesOnRequests(int organizationId)
        {
            return this._userResponseService.GetUserResponsesOnRequests(organizationId);
        }

        /// <summary>
        /// Changes the user response status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <returns></returns>
        [HttpPost("ChangeUserResponseStatus")]
        public UserResponsesOnRequestsViewModel ChangeUserResponseStatus([FromBody]UserResponseChangeStatusViewModel newStatus)
        {
            return this._userResponseService.ChangeUserResponseStatus(newStatus);
        }

        /// <summary>
        /// Gets the count new status.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        [HttpGet("GetUserResonseWithNewStatus/{organizationId}")]
        public int GetCountNewStatus(int organizationId)
        {
            return this._userResponseService.GetUserResponseWithNewStatus(organizationId);
        }
    }
}