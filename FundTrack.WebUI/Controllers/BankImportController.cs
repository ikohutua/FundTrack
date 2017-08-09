﻿using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;


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
        public ImportPrivatViewModel RegisterBankExtracts([FromBody]ImportPrivatViewModel bankModel)
        {
            return this._service.RegisterBankExtracts(bankModel);
        }
    }
}