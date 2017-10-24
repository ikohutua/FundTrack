using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        /// Gets or Sets Url to image with logo
        /// </summary>
        public string LogoUrl { get; set; }

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

        /// <summary>
        /// Gets or Sets Target navigation property
        /// </summary>
        public virtual ICollection<Target> Targets { get; set; }

        /// <summary>
        /// Gets or Sets auto import interval navigation property
        /// </summary>
        public virtual AutoImportIntervals AutoImportInterval { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Organization");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
