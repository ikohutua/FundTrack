using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller witch works with list of organizations
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class OrganizationsListController : Controller
    {
        private IOrganizationsForFilteringService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsListController"/> class.
        /// </summary>
        /// <param name="serviceParam">The service parameter.</param>
        public OrganizationsListController(IOrganizationsForFilteringService serviceParam)
        {
            this._service = serviceParam;
        }

        /// <summary>
        /// Sends to Angular service collection of 'OrganizationForSearch'
        /// </summary>
        /// <returns> collection of 'OrganizationForSearch' </returns>
        [HttpGet]
        public IEnumerable<OrganizationForFilteringViewModel> AllOrganizations()
        {
            return this._service.GetAll();
        }
    }
}
