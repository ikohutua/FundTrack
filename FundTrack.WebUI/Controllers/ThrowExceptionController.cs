using System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundTrack.WebUI.Controllers
{
    //Controller to test exception handling. Will be deleted when logging is being completed.
    public class ThrowExceptionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            throw new Exception();
        }
    }
}
