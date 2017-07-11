using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// This is a controller for requested item
    /// </summary>
    [Produces("application/json")]
    [Route("api/RequestedItem")]
    public class RequestedItemController : Controller
    {
        private readonly IRequestedItemService _requestedItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedItemController"/> class.
        /// </summary>
        /// <param name="requestedItemService">The requested item service.</param>
        public RequestedItemController(IRequestedItemService requestedItemService)
        {
            this._requestedItemService = requestedItemService;
        }

        /// <summary>
        /// Gets the request item detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RequestedItemDetailViewModel</returns>
        [HttpGet("GetRequestDetail/{id}")]
        public RequestedItemDetailViewModel GetRequestItemDetail(int id)
        {
            return this._requestedItemService.GetRequestedItemDetail(id);
          
        }

        /// <summary>
        /// Sets the user response.
        /// </summary>
        /// <param name="userResponse">The user response.</param>
        /// <returns>UserResponseViewModel</returns>
        [HttpPost("SetUserResponse")]
        public UserResponseViewModel SetUserResponse([FromBody] UserResponseViewModel userResponse)
        {
            return this._requestedItemService.CreateUserResponse(userResponse);
        }
    }
}
