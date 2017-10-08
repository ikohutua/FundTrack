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
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        [HttpPost("CreateFinOp")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult CreateFinOp([FromBody] FinOpFromBankViewModel finOpModel)
        {
            if(finOpModel == null)
            {
                return BadRequest();
            }
            return Ok(_service.CreateFinOp(finOpModel));
        }

        /// <summary>
        /// Gets the fin ops by org acc identifier.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        [HttpGet("GetFinOpsByOrgAccId/{orgAccountId}")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult GetFinOpsByOrgAccId(int orgAccountId)
        {
            if (orgAccountId <= 0)
            {
                return BadRequest();
            }
            return Ok(_service.GetFinOpsByOrgAccount(orgAccountId));
        }

        [HttpGet("GetFinOpsById/{id}")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult GetFinOpsById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            return Ok(_service.GetFinOpsById(id));
        }

        [HttpPost("Income")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Income([FromBody] FinOpViewModel incomeModel)
        {
            if(incomeModel == null)
            {
                return BadRequest();
            }
            return Ok(_service.CreateIncome(incomeModel));
        }

        [HttpPost("Spending")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Spending([FromBody] FinOpViewModel spendingModel)
        {
            if (spendingModel == null)
            {
                return BadRequest();
            }
            return Ok(_service.CreateSpending(spendingModel));
        }

        [HttpPost("Transfer")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Transfer([FromBody] FinOpViewModel transferModel)
        {
            if (transferModel == null)
            {
                return BadRequest();
            }
            return Ok(_service.CreateTransfer(transferModel));
        }

        [HttpPut()]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Edit([FromBody] FinOpViewModel finOp)
        {
            if (finOp == null)
            {
                return BadRequest();
            }
            return Ok(_service.EditFinOperation(finOp));
        }
        
        [HttpGet("{orgId}")]
        public IActionResult GetAllFinOpsByOrganizationId(int orgId)
        {
            if (orgId <= 0)
            {
                BadRequest();
            }
            return Ok(_service.GetAllFinOpsByOrgId(orgId));
        }
        
        [HttpPost("bindDonationAndFinOp")]
        public IActionResult BindDonationAndFinOp([FromBody] FinOpViewModel finOp)
        {
            if (finOp == null)
            {
                return BadRequest();
            }
            return Ok(_service.BindDonationAndFinOp(finOp));
        }
    }
}
