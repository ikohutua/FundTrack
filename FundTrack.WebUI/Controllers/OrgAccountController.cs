using FundTrack.BLL.Abstract;
using FundTrack.BLL.Concrete;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class OrgAccountController : Controller
    {
        private readonly IOrganizationAccountService _orgAccountService;
        public OrgAccountController(IOrganizationAccountService orgAccountService)
        {
            _orgAccountService = orgAccountService;
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("[action]/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_orgAccountService.GetOrganizationAccountById(id));
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("[action]/{orgId}")]
        public IActionResult ReadAll(int orgId)
        {
            return Ok(_orgAccountService.GetAccountsByOrganizationId(orgId));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public IActionResult Create([FromBody]OrgAccountViewModel model)
        {
            var item = _orgAccountService.CreateOrganizationAccount(model);
            return Ok(item);
        }

        [HttpPut("[action]")]
        public IActionResult Edit([FromBody]OrgAccountViewModel model)
        {
            return Ok(_orgAccountService.UpdateOrganizationAccount(model));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public DeleteOrgAccountViewModel Delete([FromBody]DeleteOrgAccountViewModel model)
        {
            return _orgAccountService.DeleteOrganizationAccount(model);
        }

        /// <summary>
        /// Gets the org accounts for fin op.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        [HttpGet("GetOrgAccountForFinOp/{orgId}/{cardNumber}")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult GetOrgAccountsForFinOp(int orgId, string cardNumber)
        {
            return Ok(_orgAccountService.GetAccountForSelect(orgId, cardNumber));
        }

        [HttpGet("GetDonationStatus/{orgAccountId}")]
        public IActionResult GetDonationStatus(int orgAccountId)
        {
            return Ok(_orgAccountService.IsDonationConnected(orgAccountId));
        }

        [HttpGet("CheckDonateFunction/{orgAccountId}")]
        public IActionResult CheckDonateFunction(int orgAccountId)
        {
            return Ok(_orgAccountService.IsDonationEnabled(orgAccountId));
        }

        [HttpGet("GetDonateCredentials/{orgAccountId}")]
        public IActionResult GetDonateCredentials(int orgAccountId)
        {
            return Ok(_orgAccountService.GetDonateCredentials(orgAccountId));
        }

        [HttpPut("[action]")]
        public IActionResult ToggleDonateFunction([FromBody]int orgAccountId)
        {
            return Ok(_orgAccountService.ToggleDonateFunction(orgAccountId));
        }

        [HttpGet("GetBankAccountId/{orgAccountId}")]
        public IActionResult GetBankAccountId(int orgAccountId)
        {
            return Ok(_orgAccountService.GetBankAccountIdByOrgAccountId(orgAccountId));
        }

        [HttpPost("[action]")]
        public BankAccountDonateViewModel ConnectDonation([FromBody]BankAccountDonateViewModel item)
        {
            return _orgAccountService.ConnectDonateFunction(item);
        }

        [HttpPut("DisableDonateFunction")]
        public BankAccountDonateViewModel DisableDonateFunction([FromBody]int bankAccountId)
        {
            return _orgAccountService.DisableDonateFunction(bankAccountId);
        }

        [HttpPut("UpdateOrganizationAccount")]
        public OrgAccountViewModel UpdateOrganizationAccount([FromBody] OrgAccountViewModel account)
        {
            return _orgAccountService.UpdateOrganizationAccount(account);
        }

        [HttpGet("ExtractCredentials/{orgAccountId}")]
        public IActionResult ExtractCredentials(int? orgAccountId)
        {
            if (HasNonNegativeValue(orgAccountId))
            {
                return BadRequest();
            }
            try
            {
                return Ok(_orgAccountService.ExtractCredentials(orgAccountId.Value));
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExtractStatus/{orgAccountId}")]
        public IActionResult ExtractStatus(int orgAccountId)
        {
            if (orgAccountId<0)
            {
                return BadRequest();
            }
            return Ok(_orgAccountService.IsExtractsConnected(orgAccountId));
        }

        [HttpPost("[action]")]
        public IActionResult ConnectExtracts([FromBody]BankAccountDonateViewModel item)
        {
            try
            {
                return Ok(_orgAccountService.ConnectExtractsFunction(item));
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("[action]")]
        public IActionResult ToggleExtractsFunction([FromBody]int? orgAccountId)
        {
            if (HasNonNegativeValue(orgAccountId))
            {
                return BadRequest(orgAccountId);
            }
            return Ok(_orgAccountService.ToggleExtractsFunction(orgAccountId.Value));
        }

        [HttpPut("[action]")]
        public IActionResult DisableExtractsFunction([FromBody]int? bankAccountId)
        {
            if (HasNonNegativeValue(bankAccountId))
            {
                return BadRequest(bankAccountId);
            }
            return Ok(_orgAccountService.DisableExtractsFunction(bankAccountId.Value));
        }

        [HttpGet("CheckExtractsFunction/{orgAccountId}")]
        public IActionResult CheckExtractsFunction(int orgAccountId)
        {
            if (orgAccountId < 0)
            {
                return BadRequest();
            }
            return Ok(_orgAccountService.IsExtractsEnabled(orgAccountId));
        }

        [HttpGet("BankAccontsAvailable/{orgId}")]
        public IActionResult GetBankAccontsWithImportCount(int orgId)
        {
            if (orgId<0)
            {
                return BadRequest();
            }
            return Ok(_orgAccountService.IsBankAccountsWithImportAvailable(orgId));
        }

        private bool HasNonNegativeValue(int? value)
        {
            return !value.HasValue || value < 0;
        }
    }
}
