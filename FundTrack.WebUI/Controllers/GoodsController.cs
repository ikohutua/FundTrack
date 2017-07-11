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
    public class GoodsController : Controller
    {
        private readonly IGoodsService _goodsService;
        public GoodsController(IGoodsService goodsService)
        {
            this._goodsService = goodsService;
        }
        /// <summary>
        /// Gets all offered items of a user with specified id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>json of offer item view models</returns>
        [HttpGet("[action]")]
        public JsonResult AllCategories()
        {
            return Json(this._goodsService.GetAllGoodsCategories());
        }
        [HttpGet("[action]")]
        public JsonResult AllTypes()
        {
            return Json(this._goodsService.GetAllGoodsTypes());
        }

    }
}
