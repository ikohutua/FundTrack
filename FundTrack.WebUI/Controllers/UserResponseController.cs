using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// This is a controller for user responses
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
        [Authorize]
        [HttpPost("ChangeUserResponseStatus")]
        public UserResponsesOnRequestsViewModel ChangeUserResponseStatus([FromBody]UserResponseChangeStatusViewModel newStatus)
        {
            return this._userResponseService.ChangeUserResponseStatus(newStatus);
        }

        /// <summary>
        /// Gets the count user response which is new
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        [HttpGet("GetUserResonseWithNewStatus/{organizationId}")]
        public int GetCountNewStatus(int organizationId)
        {
            return this._userResponseService.GetUserResponseWithNewStatus(organizationId);
        }

        /// <summary>
        /// Gets UserResponse Data for pagination
        /// </summary>
        /// <returns>Organizations pagination data</returns>
        [HttpGet("GetUserResponsePaginationData/{organizationId}")]
        public int GetRequestedItemPaginationData(int organizationId)
        {
            return this._userResponseService.GetRequestedItemPaginationData(organizationId);
        }

        /// <summary>
        /// Gets list of user response per page
        /// </summary>
        /// <param name="organizationId">Organization id</param>
        /// <param name="currentPage">Current page number</param>
        /// <param name="itemsPerPage">Page size number</param>
        /// <returns>Requested items list</returns>
        [HttpGet("GetUserResponseToShowPerPage/{organizationId}/{currentPage}/{itemsPerPage}")]
        public IEnumerable<UserResponsesOnRequestsViewModel> GetUserResponsePagination(int organizationId,int currentPage, int itemsPerPage)
        {
            return this._userResponseService.GetRequestedItemToShowPerPage(organizationId, itemsPerPage, currentPage);
        }

        /// <summary>
        /// Delete user response
        /// </summary>
        /// <param name="responseId"></param>
        [Authorize]
        [HttpDelete("DeleteUserResponse/{responseId}")]
        public JsonResult DeleteUserResponse(int responseId)
        {
            try
            {
                this._userResponseService.DeleteUserResponse(responseId);

                return new JsonResult(string.Empty);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}