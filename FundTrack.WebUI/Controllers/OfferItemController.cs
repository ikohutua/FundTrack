using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller, that handles operations for User Offer Items
    /// </summary>
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly IOfferedItemService _offerService;
        public OfferController(IOfferedItemService offerService)
        {
            this._offerService = offerService;
        }
        /// <summary>
        /// Gets all offered items of a user with specified id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>json of offer item view models</returns>
        [HttpGet("[action]/{id}")]
        public JsonResult Get(int id)
        {
            return Json(this._offerService.GetUserOfferedItems(id));
        }
        /// <summary>
        /// Creates offer item from offer item view model
        /// </summary>
        /// <param name="model">Offer item view model</param>
        /// <returns>Offer item view model, received from created offer item</returns>
        [HttpPost("[action]")]
        public JsonResult Create([FromBody]OfferedItemViewModel model)
        {
            this._offerService.CreateOfferedItem(model);
            return Json(model);
        }
        /// <summary>
        /// Gets all user offers by user's id
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <returns>All offer item view models of a user, whose id matches with entered</returns>
        
        [HttpGet("[action]/{id}")]
        public JsonResult GetUserOfferById(int id)
        {
            return Json(this._offerService.GetOfferedItemViewModel(id));
        }
        /// <summary>
        /// Removes specified offer item from database
        /// </summary>
        /// <param name="id">id of the offer item</param>
        /// <returns>200 status code if delete succeded</returns>

        [HttpDelete("[action]/{id}")]
        public StatusCodeResult Delete(int id)
        {
            this._offerService.DeleteOfferedItem(id);
            return StatusCode(200);
        }
        /// <summary>
        /// Edits specified offer item, by received offer item view model
        /// </summary>
        /// <param name="model">offer item view model received from front end</param>
        /// <returns>Edited offer item view model from offer item object</returns>
       
        [HttpPut("[action]")]
        public JsonResult Edit([FromBody]OfferedItemViewModel model)
        {
           return Json(this._offerService.EditOfferedItem(model));
        }
        /// <summary>
        /// Returns specified amount of events, skipping specified amount
        /// </summary>
        /// <param name="itemsLoadCount">amount of items to be loaded</param>
        /// <param name="itemsLoadRatio">amount of items to skip from the beginning</param>
        /// <returns>Offer Item View Models</returns>
 
        [HttpGet("GetOfferedItemPortion/{userId}/{itemsLoadCount}/{itemsLoadRatio}")]
        public JsonResult GetOfferedItemPortion(int userId,int itemsLoadCount, int itemsLoadRatio)
        {
            var result = Json(_offerService.GetPagedUserOfferedItems(userId, itemsLoadCount, itemsLoadRatio));
            return result;
        }

        [HttpPost("[action]")]
        public JsonResult ChangeOfferItemStatus([FromBody] OfferItemChangeStatusViewModel model)
        {
            return Json(this._offerService.ChangeOfferItemStatus(model));
        }
    }
        
}
