using FundTrack.BLL.Abstract;
using Microsoft.AspNetCore.Mvc;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class BankController : Controller
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public JsonResult GetAllBanks()
        {
            return Json(_bankService.GetAllBanks());
        }

        [HttpGet("{bankId}")]
        public BankViewModel GetBankById(int bankId)
        {
            return _bankService.GetBankById(bankId);
        }
    }
}
