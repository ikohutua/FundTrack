using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Class which describe model of Organization
    /// </summary>
    public class OrganizationViewModel
    {
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
        /// Indicates if that the Organization is banned
        /// </summary>
        public bool IsBanned { get; set; }
    }
}
