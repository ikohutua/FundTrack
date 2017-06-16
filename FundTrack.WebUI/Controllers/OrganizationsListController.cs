using FundTrack.Infrastructure.ViewModel;
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
        /// <summary>
        /// Sends to Angular service collection of 'OrganizationForSearch'
        /// </summary>
        /// <returns> collection of 'OrganizationForSearch' </returns>
        [HttpGet]
        public IEnumerable<OrganizationForSearch> AllOrganizations()
        {
            //HARDCODE
            return new List<OrganizationForSearch>()
            {
                new OrganizationForSearch(){ Id = 1, Name = "Наш солдат" },
                new OrganizationForSearch(){ Id = 1, Name = "Крила Фенікса" },
                new OrganizationForSearch(){ Id = 1, Name = "Інша" },
                new OrganizationForSearch(){ Id = 1, Name = "Інша" },
                new OrganizationForSearch(){ Id = 1, Name = "Крила" },
                new OrganizationForSearch(){ Id = 1, Name = "Інша" },
                new OrganizationForSearch(){ Id = 1, Name = "Інша" },
                new OrganizationForSearch(){ Id = 1, Name = "Крила" },
                new OrganizationForSearch(){ Id = 1, Name = "Інша" },
                new OrganizationForSearch(){ Id = 1, Name = "Інша" },
            };
        }
    }
}
