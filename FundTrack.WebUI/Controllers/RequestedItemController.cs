using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Concrete;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.BLL.Abstract;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Requested item controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/RequestedItem")]
    public class RequestedItemController : Controller
    {
        private readonly IRequestedItemService _requestedItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedItemController"/> class.
        /// </summary>
        /// <param name="requestedItemService">Requested item service</param>
        public RequestedItemController(IRequestedItemService requestedItemService)
        {
            this._requestedItemService = requestedItemService;
        }

        /// <summary>
        /// Add requested item
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Json result</returns>
        [HttpPost("[action]")]
        public JsonResult AddRequestedItem([FromBody]RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                this._requestedItemService.CreateRequestedItem(requestedItemViewModel);
                return new JsonResult(string.Empty);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        /// <summary>
        /// Update requested item
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Json result</returns>
        [HttpPut("[action]")]
        public JsonResult UpdateRequestedItem([FromBody]RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                this._requestedItemService.UpdateRequestedItem(requestedItemViewModel);

                return new JsonResult(string.Empty);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        /// <summary>
        /// Delete requested item 
        /// </summary>
        /// <param name="requestedItemViewModel">Id of requested item view model</param>
        /// <returns>Json result</returns>
        [HttpDelete("DeleteRequestedItem/{itemId}")]
        public JsonResult DeleteRequestedItem(int itemId)
        {
            try
            {
                this._requestedItemService.DeleteRequestedItem(itemId);

                return new JsonResult(string.Empty);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        /// <summary>
        /// Gets all requested items
        /// </summary>
        /// <returns>List of all requested items</returns>
        [HttpGet("GetOrganizationRequestedItems/{organizationId}")]
        public IEnumerable<RequestedItemViewModel> GetOrganizationRequestedItems(int organizationId)
        {
            List<RequestedItemViewModel> allItems = this._requestedItemService.GetOrganizationRequestedId(organizationId);

            return allItems;
        }

        /// <summary>
        /// Gets requested item by id
        /// </summary>
        /// <param name="id">Id og requested item</param>
        /// <returns>Requested item view model</returns>
        [HttpGet("GetRequestedItem/{id}")]
        public RequestedItemViewModel GetRequestedItem(int id)
        {
            RequestedItemViewModel requestedItemViewModel = this._requestedItemService.GetItemById(id);

            return requestedItemViewModel;
        }

        /// <summary>
        /// Gets goods type
        /// </summary>
        /// <returns>List of goods type</returns>
        [HttpGet("[action]")]
        public IEnumerable<GoodsTypeViewModel> GetGoodsType()
        {           
                var goodsType = this._requestedItemService.GetAllGoodTypes();
                return goodsType;                         
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

    
