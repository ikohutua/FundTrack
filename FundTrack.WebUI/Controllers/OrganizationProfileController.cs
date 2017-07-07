using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

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
    }
}
