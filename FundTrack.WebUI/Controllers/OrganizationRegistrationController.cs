using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller to create new organization
    /// </summary>
    [Route("api/[controller]")]
    public class OrganizationRegistrationController : Controller
    {
        private readonly IOrganizationRegistrationService _orgRegisterService; 

        /// <summary>
        /// Creates new instance of OrganizationRegistrationController
        /// </summary>
        /// <param name="orgRegisterService"> IOrganizationRegistrationService to create new instance</param>
        public OrganizationRegistrationController (IOrganizationRegistrationService orgRegisterService)
        {
            _orgRegisterService = orgRegisterService;
        }

        /// <summary>
        /// Creates new organization
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns>Created item</returns>
        [HttpPost("[action]")]
        public OrganizationRegistrationViewModel RegisterNewOrganization ([FromBody]OrganizationRegistrationViewModel item)
        {
            return _orgRegisterService.RegisterOrganization(item);
        }
    }
}
