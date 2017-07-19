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
    }
}