using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using System.Collections.Generic;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller that manages operations with organization profile
    /// </summary>
    [Route("api/[controller]")]
    public class OrganizationProfileController:Controller
    {
        private readonly IOrganizationProfileService _orgProfileService; 
        /// <summary>
        /// Cretes new instance of Organization Profile controller
        /// </summary>
        /// <param name="orgProfileService"> Service parapemeter to create instance</param>
        public OrganizationProfileController (IOrganizationProfileService orgProfileService)
        {
            _orgProfileService = orgProfileService;
        }

        /// <summary>
        /// Gets organization name and description by its id
        /// </summary>
        /// <param name="id">Id of organization</param>
        /// <returns>Organization View Model</returns>
        [HttpGet("GetInformationById/{id}")]
        public OrganizationViewModel GetInformationById(int id)
        {
            return _orgProfileService.GetOrganizationById(id);
        }

        /// <summary>
        /// Editds description of organization
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("EditDescription")]
        public OrganizationViewModel EditDesciption([FromBody]OrganizationViewModel item)
        {
            return _orgProfileService.EditDescription(item);
        }

        [HttpGet("GetAddress/{id}")]
        public EditAddressViewModel GetAddress(int id)
        {
            return _orgProfileService.GetOrgAddress(id);
        }

        [HttpPost("AddAddresses")]
        public EditAddressViewModel AddAddresses([FromBody] EditAddressViewModel addresses)
        {
            return _orgProfileService.AddAddresses(addresses);
        }

        [HttpDelete("DeleteAddress/{id}")]
        public void DeleteAddress(int id)
        {
            _orgProfileService.DeleteAddress(id);
        }

        [HttpPut("EditAddress")]
        public EditAddressViewModel EditAddress([FromBody] AddressViewModel address)
        {
            return _orgProfileService.EditAddress(address);
        }
    }
}
