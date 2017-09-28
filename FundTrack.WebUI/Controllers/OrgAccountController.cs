using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class OrgAccountController:Controller
    {
        private readonly IOrganizationAccountService _orgAccountService;
        public OrgAccountController(IOrganizationAccountService orgAccountService)
        {
            this._orgAccountService = orgAccountService;
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpGet("[action]/{id}")]
        public JsonResult Get(int id)
        {
            return Json(this._orgAccountService.GetOrganizationAccountById(id));
        }
        [Authorize(Roles ="admin, moderator")]
        [HttpGet("[action]/{orgId}")]
        public JsonResult ReadAll(int orgId)
        {
            return Json(this._orgAccountService.GetAccountsByOrganizationId(orgId));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public JsonResult Create([FromBody]OrgAccountViewModel model)
        {
            var item= this._orgAccountService.CreateOrganizationAccount(model);
            return Json(item);
        }
        [HttpPut("[action]")]
        public JsonResult Edit([FromBody]OrgAccountViewModel model)
        {
            return Json(this._orgAccountService.UpdateOrganizationAccount(model));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public DeleteOrgAccountViewModel Delete([FromBody]DeleteOrgAccountViewModel model)
        {
            return this._orgAccountService.DeleteOrganizationAccount(model);
        }

        /// <summary>
        /// Gets the org accounts for fin op.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        [HttpGet("GetOrgAccountForFinOp/{orgId}/{cardNumber}")]
        [Authorize(Roles = "admin, moderator")]
        public JsonResult GetOrgAccountsForFinOp(int orgId,string cardNumber)
        {
            return Json(this._orgAccountService.GetAccountForSelect(orgId,cardNumber));
        }

        [HttpGet("GetDonationStatus/{orgAccountId}")]        
        public JsonResult GetDonationStatus(int orgAccountId)
        {
            return Json(_orgAccountService.IsDonationConnected(orgAccountId));
        }

        [HttpGet("CheckDonateFunction/{orgAccountId}")]
        public JsonResult CheckDonateFunction(int orgAccountId)
        {
            return Json(_orgAccountService.IsDonationEnabled(orgAccountId));
        }

        [HttpGet("GetDonateCredentials/{orgAccountId}")]
        public JsonResult GetDonateCredentials(int orgAccountId)
        {
            return Json(_orgAccountService.GetDonateCredentials(orgAccountId));
        }

        [HttpPut("[action]")]
        public JsonResult ToggleDonateFunction([FromBody]int orgAccountId)
        {
             return Json(_orgAccountService.ToggleDonateFunction(orgAccountId));
        }

        [HttpGet("GetBankAccountId/{orgAccountId}")]
        public int GetBankAccountId(int orgAccountId)
        {
            return _orgAccountService.GetBankAccountIdByOrgAccountId(orgAccountId);
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
        public JsonResult ExtractCredentials(int orgAccountId)
        {
            return Json(_orgAccountService.ExtractCredentials(orgAccountId));
        }

        [HttpGet("ExtractStatus/{orgAccountId}")]
        public JsonResult ExtractStatus(int orgAccountId)
        {
            return Json(_orgAccountService.IsExtractEnabled(orgAccountId));
        }

        [HttpPost("[action]")]
        public BankAccountDonateViewModel ConnectExtracts([FromBody]BankAccountDonateViewModel item)
        {
            return _orgAccountService.ConnectExtractsFunction(item);
        }
    }
}
