using System;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Bank Import controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/BankImport")]
    public class BankImportController : Controller
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IBankImportService _service;

        /// <summary>
        /// Initializes a new instance of the "EventController" class
        /// </summary>
        /// <param name="service">The instance of service.</param>
        public BankImportController(IBankImportService service)
        {
            _service = service;
        }

        /// <summary>
        /// Registers the bank extracts.
        /// </summary>
        /// <param name="bankImport">The bank import.</param>
        /// <returns></returns>
        [HttpPost("RegisterNewExtracts")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<ImportDetailPrivatViewModel> RegisterBankExtracts([FromBody]ImportDetailPrivatViewModel[] bankModel)
        {
            return this._service.RegisterBankExtracts(bankModel);
        }

        /// <summary>
        /// Gets the  extracts which satisfy filters.
        /// </summary>
        /// <param name="bankSearchModel">The bank search model.</param>
        /// <returns></returns>
        [HttpPost("SearchRawExtractsOnPeriod")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<ImportDetailPrivatViewModel> GetRawExtracts([FromBody] BankImportSearchViewModel bankSearchModel)
        {
            return this._service.GetRawExtracts(bankSearchModel);
        }


        /// <summary>
        /// Gets all extracts in one orgaccount.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        [HttpGet("GetAllExtracts/{card}")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<ImportDetailPrivatViewModel> GetAllExtracts(string card)
        {
            return this._service.GetAllExtracts(card);
        }

        /// <summary>
        /// Gets the count extracts in one orgaccount.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        [HttpGet("GetCountExtracts/{card}")]
        [Authorize(Roles = "admin, moderator")]
        public int GetCountExtracts(string card)
        {
            return this._service.GetCountExtracts(card);
        }

        [HttpPost("ImportPrivat")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult ImportPrivat([FromBody]int orgAccountId)
        {
            if (orgAccountId<=0)
            {
                return BadRequest();
            }
            return Ok(_service.ImportFromPrivat(orgAccountId));
        }

        [HttpGet("LastUpdate/{orgId}")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult LastUpdate (int orgId)
        {
            if (orgId <= 0)
            {
                return BadRequest();
            }
            return Ok(_service.GetLastPrivatUpdate(orgId));
        }

        [HttpGet("UpdateDate/{orgId}")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult UpdateDate(int orgId)
        {
            if (orgId <= 0)
            {
                return BadRequest();
            }
            return Ok(_service.UpdateDate(orgId).LastUpdateDate);
        }

        [HttpPost("Privat")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult PrivatImport([FromBody] PrivatImportViewModel model)
        {
            try
            {
                return Ok(_service.ImportWithDates(model));
            }
            catch (Exception ex)
            {
                throw new  Exception(ex.Message, ex);
            }
        }

        [HttpGet("SuggestedImports/{amount}/{date}")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult GetAllSuggestedBankImports(decimal amount, DateTime date)
        {
            if (amount == 0)
            {
                return BadRequest();
            }
            return Ok(_service.getAllSuggestedBankImports(amount,date));
        }
        [HttpPut("UpdateInterval")]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult UpdateInterval([FromBody]AutoImportIntervalViewModel interval)
        {
            if (interval ==null)
            {
                return BadRequest();
            }
            return Ok(_service.UpdateInterval(interval));
        }
    }
}
