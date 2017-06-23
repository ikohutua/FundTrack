using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Entities
{
    public class SubscribeOrganization
    {
        /// <summary>
        /// Subscribe organization id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Organization Id
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property user
        /// </summary>
        public virtual User User { get; set; }
        
        /// <summary>
        /// Navigation property organization
        /// </summary>
        public virtual Organization Organization {get; set;}
    }
}
