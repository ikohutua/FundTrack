using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Concrete;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.RequestedItemModel;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Requested item controller class
    /// </summary>
    [Route("api/[controller]")]
    public sealed class RequestedItemController : Controller
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
        public RequestedItemViewModel AddRequestedItem([FromBody]RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var a = HttpContext.Items;
                    return this._requestedItemService.CreateRequestedItem(requestedItemViewModel);                 
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                    string generalError = string.Join(" ", errors);
                    return new RequestedItemViewModel
                    {
                        ErrorMessage = generalError
                    };
                }
            }
            catch (Exception ex)
            {
                return new RequestedItemViewModel
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Update requested item
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Json result</returns>
        [HttpPut("[action]")]
        public RequestedItemViewModel UpdateRequestedItem([FromBody]RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var requestedItem = this._requestedItemService.UpdateRequestedItem(requestedItemViewModel);

                    return requestedItem;
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                    string generalError = string.Join(" ", errors);

                    return new RequestedItemViewModel
                    {
                        ErrorMessage = generalError
                    };
                }
            }
            catch (Exception ex)
            {
                return new RequestedItemViewModel
                {
                    ErrorMessage = ex.Message
                };
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
            try
            {
                List<RequestedItemViewModel> allItems = this._requestedItemService.GetOrganizationRequestedId(organizationId);
                return allItems;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets requested item by id
        /// </summary>
        /// <param name="id">Id og requested item</param>
        /// <returns>Requested item view model</returns>
        [HttpGet("GetRequestedItem/{id}")]
        public RequestedItemViewModel GetRequestedItem(int id)
        {
            try
            {
                RequestedItemViewModel requestedItemViewModel = this._requestedItemService.GetItemById(id);

                return requestedItemViewModel;
            }
            catch(Exception ex)
            {
                return new RequestedItemViewModel()
                {
                    ErrorMessage = ex.Message
                };
            }
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

        [HttpGet("[action]")]
        public IEnumerable<OrganizationForFilteringViewModel> GetOrganizations()
        {
            return _requestedItemService.GetOrganizations(); 
        }

        [HttpGet("[action]")]
        public IEnumerable<GoodsCategoryForFilteringViewModel> GetCategories()
        {
            return _requestedItemService.GetCategories();
        }

        [HttpGet("[action]")]
        public IEnumerable<GoodsTypeForFilteringViewModel> GetTypes()
        {
            return _requestedItemService.GetTypes();
        }

        [HttpGet("[action]")]
        public IEnumerable<StatusForFilteringViewModel> GetStatuses()
        {
            return _requestedItemService.GetStatuses();
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

        /// <summary>
        /// Delete currentImage from database
        /// </summary>
        /// <param name="currentImageId">Current image id</param>
        [HttpDelete("DeleteCurrentImage/{currentImageId}")]
        public JsonResult DeleteCurrentImage(int currentImageId)
        {
            try
            {
                this._requestedItemService.DeleteCurrentImage(currentImageId);

                return new JsonResult(string.Empty);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        /// <summary>
        /// All the requested items of all organizations with additional information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns to WEB info about all events of specific organization</returns>
        [HttpGet("GetRequestedItemToShow")]
        public IEnumerable<ShowAllRequestedItemModel> GetRequestedItemToShow()
        {
            return _requestedItemService.GetRequestedItemToShow();
        }

        /// <summary>
        /// The count of requested items per page with additional information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns to WEB info about count of requested items per page with additional information.</returns>
        [HttpPost("GetRequestedItemToShowPerPage")]
        public IEnumerable<ShowAllRequestedItemModel> GetRequestedItemToShowPerPage([FromBody] FilterPaginationViewModel filters)
        {
            return _requestedItemService.GetRequestedItemToShowPerPage(filters);
        }

        /// <summary>
        /// Gets RequestedItem Data for pagination
        /// </summary>
        /// <returns>Organizations pagination data</returns>
        [HttpGet("GetRequestedItemPaginationData")]
        public RequestedItemPaginationInitViewModel GetRequestedItemPaginationData()
        {
            return _requestedItemService.GetRequestedItemPaginationData();
        }

        /// <summary>
        /// Gets list of requested items per page
        /// </summary>
        /// <param name="organizationId">Organization id</param>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size number</param>
        /// <returns>Requested items list</returns>
        [HttpGet("GetRequestedItemPerPage/{organizationId}/{currentPage}/{pageSize}")]
        public IEnumerable<RequestedItemViewModel> GetRequestedItemPerPage(int organizationId, int currentPage, int pageSize)
        {
          
            return this._requestedItemService.GetRequestedItemPerPageByorganizationId(organizationId, currentPage, pageSize);
        }

        /// <summary>
        /// Gets requested item pagination view model
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns>Pagination view model</returns>
        [HttpGet("GetRequestedItemInitData/{organizationId}")]
        public RequestedItemPaginationViewModel GetRequestedItemInitData(int organizationId)
        {
            return this._requestedItemService.GetRequestedItemsInitData(organizationId);
        }

        /// <summary>
        /// Gets filter requested item pagination view model
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        [HttpPost("GetFilterRequestedItemPaginationData")]
        public RequestedItemPaginationInitViewModel GetFilterRequestedItemPaginationData([FromBody] FilterRequstedViewModel filters)
        {
            return this._requestedItemService.GetFilterRequestedItemPaginationData(filters);
        }
    }
}


    
