using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Net.Http;
using System;
using FundTrack.Infrastructure;

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

        /// <summary>
        /// Gets organization detail by id
        /// </summary>
        /// <param name="id">Organization id</param>
        /// <returns>Requested organization detail view model</returns>
        [HttpGet("{id}")]
        public IActionResult OrganinzationDetailByOrgId(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            return Ok(organizationProfileService.GetOrganizationDetail((int)id));
        }

    }
}
