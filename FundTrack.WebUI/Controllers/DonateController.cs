using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.AspNetCore.Authorization;
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
    public class DonateController : Controller
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
        [HttpGet("AccountsForDonate/{organizationId}")]
        public OrganizationDonateAccountsViewModel GetAccountsForDonate(int organizationId)
        {
            return _donateMoneyService.GetAccountForDonation(organizationId);
        }

        [HttpGet("OrderId")]
        public string GetOrderId()
        {
            return _donateMoneyService.GetOrderId();
        }

        [HttpGet("Currencies")]
        public IEnumerable<CurrencyViewModel> GetCurrencies()
        {
            return _donateMoneyService.GetCurrencies();
        }

        [HttpPost("AddDonation")]
        public DonateViewModel AddDonation([FromBody]DonateViewModel item)
        {
            return _donateMoneyService.AddDonation(item);
        }

        [HttpGet]
        public IEnumerable<DonateViewModel> GetAllDonations()
        {
            return _donateMoneyService.GetAllDonatons();
        }

        [HttpGet("{id}")]
        public DonateViewModel GetDonationById(int id)
        {
            return _donateMoneyService.GetDonationById(id);
        }

        [HttpGet("suggested/{finOpId}")]
        public IEnumerable<DonateViewModel> GetSuggestedDonations(int finOpId)
        {
            return _donateMoneyService.GetSuggestedDonations(finOpId);
        }

        [HttpGet("User/{userId}")]
        [Authorize]
        public IActionResult GetUserDonations(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }
            return Ok(_donateMoneyService.GetUserDonations(userId));
        }

        [HttpGet("UserByDate")]
        [Authorize]
        public IActionResult GetUserDonationsByDate(int userId, DateTime dateFrom, DateTime dateTo)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }
            return Ok(_donateMoneyService.GetUserDonationsByDate(userId, dateFrom, dateTo));
        }
    }
}
