using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.WebUI.Controllers
{
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
        [HttpPost("[action]")]
        public JsonResult Create([FromBody]OfferedItemViewModel model)
        {
            this._offerService.CreateOfferedItem(model);
            return Json(model);
        }
        [HttpGet("[action]/{id}")]
        public JsonResult GetUserOfferById(int id)
        {
            return Json(this._offerService.GetOfferedItemViewModel(id));
        }
        [HttpDelete("[action]/{id}")]
        public StatusCodeResult Delete(int id)
        {
            this._offerService.DeleteOfferedItem(id);
            return StatusCode(200);
        }
        [HttpPut("[action]")]
        public JsonResult Edit([FromBody]OfferedItemViewModel model)
        {
           return Json(this._offerService.EditOfferedItem(model));
        }
    }
        
}
