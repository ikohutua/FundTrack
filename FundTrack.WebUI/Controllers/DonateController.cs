using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller that manages donate money operations
    /// </summary>
    [Route("api/[controller]")]
    public class DonateController: Controller
    {
        private readonly IDonateMoneyService _donateMoneyService;

        /// <summary>
        /// Instantiates controller
        /// </summary>
        /// <param name="donateMoneyService">Service to create instance of controller</param>
        public DonateController(IDonateMoneyService donateMoneyService)
        {
            _donateMoneyService = donateMoneyService;
        }

        /// <summary>
        /// Sends request to payment system
        /// </summary>
        /// <param name="request">View model with parameters for payment</param>
        /// <returns>Url to perform payment</returns>
        [HttpPost("SendRequestFondy")]
        public string SendRequestFondy([FromBody] RequestViewModel request)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.fondy.eu/api/checkout/url/")
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var response = client.PostAsync(client.BaseAddress,
                new StringContent(json, Encoding.UTF8, "application/json")).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            FondyCheckoutResponseViewModel checkout = new FondyCheckoutResponseViewModel();
            checkout = Newtonsoft.Json.JsonConvert.DeserializeObject<FondyCheckoutResponseViewModel>(content);
            return checkout.response.checkout_url;
        }

        /// <summary>
        /// Checks status of payment by order_id
        /// </summary>
        /// <param name="request">Parameters for payment system to get status of payment</param>
        /// <returns>Details about payment</returns>
        [HttpPost("CheckPayment")]
        public string CheckPayment([FromBody] CheckStatusRequestViewModel request)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.fondy.eu/api/status/order_id")
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var response = client.PostAsync(client.BaseAddress,
                new StringContent(json, Encoding.UTF8, "application/json")).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        /// <summary>
        /// Gets organization accounts available for donation
        /// </summary>
        /// <param name="organizationId">Id of organization</param>
        /// <returns>Organization id and accounts</returns>
        [HttpGet("GetAccountsForDonate/{organizationId}")]
        public OrganizationDonateAccountsViewModel GetAccountsForDonate(int organizationId)
        {
            return _donateMoneyService.GetAccountForDonation(organizationId);
        }

        [HttpGet("GetOrderId")]
        public string GetOrderId()
        {
            return _donateMoneyService.GetOrderId();
        }

        [HttpGet("GetTargets")]
        public IEnumerable<TargetViewModel> GetTargets()
        {
            return _donateMoneyService.GetTargets();
        }

        [HttpGet("GetCurrencies")]
        public IEnumerable<CurrencyViewModel> GetCurrencies()
        {
            return _donateMoneyService.GetCurrencies();
        }

        [HttpPost("AddDonation")]
        public DonateViewModel AddDonation ([FromBody]DonateViewModel item)
        {
            return _donateMoneyService.AddDonation(item);
        }

    }
}
