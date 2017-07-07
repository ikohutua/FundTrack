using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Organization entity
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Organization
        /// </summary>    
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description of Organization
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Navigation property Banned organization
        /// </summary>
        public virtual BannedOrganization BannedOrganization { get; set; }

        /// <summary>
        /// Gets or Sets Membership navigation property
        /// </summary>
        public virtual ICollection<Membership> Memberships { get; set; }

        /// <summary>
        /// Gets or Sets OrgAddresse navigation property
        /// </summary>
        public virtual ICollection<OrgAddress> OrgAddresses { get; set; }

        /// <summary>
        /// Gets or Sets BankAccount navigation property
        /// </summary>
        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        /// <summary>
        /// Gets or Sets Subscribe organization navigation property
        /// </summary>
        public virtual ICollection<SubscribeOrganization> SubscribeOrganization { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccounts navigation property
        /// </summary>
        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }

        /// <summary>
        /// Gets or Sets Request navigation property
        /// </summary>
        public virtual ICollection<RequestedItem> RequestedItems { get; set; }

        /// <summary>
        /// Gets or Sets Complaint navigation property
        /// </summary>
        public virtual ICollection<Complaint> Complaints { get; set; }

        /// <summary>
        /// Gets or Sets Event navigation property
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }

        /// <summary>
        /// Gets or Sets Event navigation property
        /// </summary>
        public virtual ICollection<OrganizationResponse> OrganizationResponses { get; set; }
    }
}
