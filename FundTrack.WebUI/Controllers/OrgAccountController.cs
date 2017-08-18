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
        [HttpPost("[action]")]
        public JsonResult ReadAll([FromBody] UserInfoViewModel model)
        {
            return Json(this._orgAccountService.GetAccountsByOrganizationId(model.orgId));
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
        /// <returns></returns>
        [HttpGet("GetOrgAccountsForFinOp/{orgId}")]
        [Authorize(Roles = "admin, moderator")]
        public JsonResult GetOrgAccountsForFinOp(int orgId)
        {
            return Json(this._orgAccountService.GetAccountsForSelectByOrganizationId(orgId));
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

    }
}
