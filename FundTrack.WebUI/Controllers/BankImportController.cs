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

        [HttpPost("SearchRawExtractsOnPeriod")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<ImportDetailPrivatViewModel> GetRawExtracts([FromBody] BankImportSearchViewModel bankSearchModel)
        {
            return this._service.GetRawExtracts(bankSearchModel);
        }

        [HttpGet("GetAllExtracts/{card}")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<ImportDetailPrivatViewModel> GetAllExtracts(string card)
        {
            return this._service.GetAllExtracts(card);
        }
    }
}
