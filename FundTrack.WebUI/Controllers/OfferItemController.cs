using FundTrack.BLL.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class OfferController:Controller
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
    }
}
