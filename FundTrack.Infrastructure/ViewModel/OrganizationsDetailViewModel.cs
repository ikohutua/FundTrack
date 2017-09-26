using System;
using System.Collections.Generic;
using System.Text;

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
        /// First name  of the organization's administrator
        /// </summary>    
        public string AdminFirstName { get; set; }

        /// <summary>
        /// Last name  of the organization's administrator
        /// </summary>    
        public string AdminLastName { get; set; }

        /// <summary>
        /// Phone number of the organization's administrator
        /// </summary>
        public string AdminPhoneNumber { get; set; }

        /// <summary>
        /// List of organization's accounts 
        /// </summary>
        public IEnumerable<OrgAccountDetailViewModel> OrgAccountsList { get; set; }

    }
}
