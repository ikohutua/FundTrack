using FundTrack.BLL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class FinOpController : Controller
    {

        /// <summary>
        /// The service
        /// </summary>
        private readonly IFinOpService _service;

        /// <summary>
        /// Initializes a new instance of the "EventController" class
        /// </summary>
        /// <param name="service">The instance of service.</param>
        public FinOpController(IFinOpService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the targets.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTargets")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<TargetViewModel> GetTargets(int id)
        {
            return this._service.GetTargets(id);
        }

        /// <summary>
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        [HttpPost("CreateFinOp")]
        [Authorize(Roles = "admin, moderator")]
        public FinOpFromBankViewModel CreateFinOp([FromBody] FinOpFromBankViewModel finOpModel)
        {
            return this._service.CreateFinOp(finOpModel);
        }

        /// <summary>
        /// Gets the fin ops by org acc identifier.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        [HttpGet("GetFinOpsByOrgAccId/{orgAccountId}")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<FinOpListViewModel> GetFinOpsByOrgAccId(int orgAccountId)
        {
            return this._service.GetFinOpsByOrgAccount(orgAccountId);
        }

        [HttpPost("Income")]
        [Authorize(Roles = "admin, moderator")]
        public FinOpViewModel Income([FromBody] FinOpViewModel incomeModel)
        {
            return this._service.CreateIncome(incomeModel);
        }

        [HttpPost("Spending")]
        [Authorize(Roles = "admin, moderator")]
        public FinOpViewModel Spending([FromBody] FinOpViewModel spendingModel)
        {
            return this._service.CreateSpending(spendingModel);
        }

        [HttpPost("Transfer")]
        [Authorize(Roles = "admin, moderator")]
        public FinOpViewModel Transfer([FromBody] FinOpViewModel transferModel)
        {
            return this._service.CreateTransfer(transferModel);
        }

    }
}
