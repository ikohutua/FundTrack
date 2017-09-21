using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class OrganizationDetailController : Controller
    {
        private readonly IOrganizationProfileService organizationProfileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationDetailController"/> class.
        /// </summary>
        /// <param name="organizationProfileService">Organization profile service</param>
        public OrganizationDetailController(IOrganizationProfileService organizationProfileService)
        {
            this.organizationProfileService = organizationProfileService;
        }

        /// <summary>
        /// Returns to WEB info about all organizations
        /// </summary>
        [HttpGet]
        public IEnumerable<OrganizationViewModel> AllOrganizations()
        {
            return organizationProfileService.GetAllOrganizations();
        }

    }
}
