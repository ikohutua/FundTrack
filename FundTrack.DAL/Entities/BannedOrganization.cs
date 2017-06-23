using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Entities
{
    public class BannedOrganization
    {
        /// <summary>
        /// Gets or sets the banned identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets organization id
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the banned organization description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }
    }
}
