using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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

        //[HttpPost("SendRequestFondy")]
        //public string SendRequestFondy([FromBody]  DataRequestPrivatViewModel request)
        //{
        //    var client = new HttpClient()
        //    {
        //        BaseAddress = new Uri("https://api.privatbank.ua/p24api/rest_fiz")
        //    };
        //    string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
        //    var response = client.PostAsync(client.BaseAddress,
        //        new StringContent(json, Encoding.UTF8, "application/json")).Result;
        //    var content = response.Content.ReadAsStringAsync().Result;
        //    FondyCheckoutResponseViewModel checkout = new FondyCheckoutResponseViewModel();
        //    checkout = Newtonsoft.Json.JsonConvert.DeserializeObject<FondyCheckoutResponseViewModel>(content);
        //    return checkout.response.checkout_url;
        //}

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
    }
}
