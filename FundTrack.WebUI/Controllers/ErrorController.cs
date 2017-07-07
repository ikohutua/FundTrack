using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundTrack.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        /// <summary>
        /// This action method takes http status code as parameter and displays corresponding View
        /// </summary>
        /// <param name="statusCode">http request status code</param>
        /// <returns>Corresponding View</returns>
        public IActionResult Index(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    return View("ErrorNotFound");

                }
                else if (statusCode == 401)
                {
                    return View("ErrorNotAuthorized");
                }
                ///other codes go here
            }
            return View("ErrorInternalServer");
        }
    }
}
