using System.Collections.Generic;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Class which describes details about organization
    /// </summary>
    public class OrganizationDetailViewModel
    {
        /// <summary>
        /// General details about organization
        /// </summary>    
        public OrganizationViewModel Organization { get; set; }

        /// <summary>
        /// General details about admin of organization
        /// </summary>  
        public PersonViewModel Admin { get; set; }

        /// <summary>
        /// List of organization's accounts 
        /// </summary>
        public IEnumerable<OrgAccountDetailViewModel> OrgAccountsList { get; set; }

    }
}
