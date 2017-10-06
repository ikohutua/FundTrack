using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// View model for registration of new organization
    /// </summary>
    public class OrganizationRegistrationViewModel
    {
        /// <summary>
        /// Id of Organization
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Organization
        /// </summary>    
        public string Name { get; set; }

        /// <summary>
        /// Description of Organization
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// City in Address
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Country in Address
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Street in Address
        /// </summary>        
        public string Street { get; set; }

        /// <summary>
        /// House in Address
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// Login of organization administrator
        /// </summary>
        public string AdministratorLogin { get; set; }

        /// <summary>
        /// Error message about user
        /// </summary>
        public string UserError { get; set; }
        
        /// <summary>
        /// Error message about name of organization
        /// </summary>
        public string NameError { get; set; }

        /// <summary>
        /// base64 code of logo
        /// </summary>
        public string Base64Code { get; set; }
    }
}
