using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.BLL.Concrete;

namespace FundTrack.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/FixingBalance")]
    [Authorize(Roles = "admin, moderator")]
    public class FixingBalanceController : Controller
    {
        private readonly IFixingBalanceService _fixingBalanceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixingBalanceController"/> class.
        /// </summary>
        /// <param name="fixingBalanceService">The fixing balance service.</param>
        public FixingBalanceController(IFixingBalanceService fixingBalanceService)
        {
            _fixingBalanceService = fixingBalanceService;
        }

        [HttpGet("{accountId}")]
        public IActionResult GetFilterByAccId(int? accountId)
        {
            if (accountId == null || accountId <= 0)
            {
                return BadRequest();
            }
            return Ok(_fixingBalanceService.GetFilteringByAccId((int)accountId));
        }

        [HttpPost]
        public IActionResult AddNewBalance([FromBody]BalanceViewModel balance)
        {
            try
            {
                return Ok(_fixingBalanceService.AddNewBalance(balance));
            }
            catch (BusinessLogicException ex)
            {   
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AllBalances")]
        public IActionResult AddNewRangeOfBalances([FromBody]AllBalances balances)
        {
            if (balances == null)
            {
                return BadRequest("balances==null");
            }
            try
            {
                return Ok(_fixingBalanceService.AddNewRangeOfBalances(balances?.Balances));
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLastFixing/{balanceId}")]
        public IActionResult DeleteLastFixing(int balanceId)
        {
            return Ok(_fixingBalanceService.DeleteLastFixing(balanceId));
        }
    }

}