using Microsoft.AspNetCore.Mvc;

namespace FundTrack_WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
