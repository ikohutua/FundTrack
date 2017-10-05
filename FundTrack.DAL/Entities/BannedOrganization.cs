using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

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

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BannedOrganization>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_BannedOrganization");
                entity.HasOne(e => e.Organization).
                    WithOne(e => e.BannedOrganization).
                    HasForeignKey<BannedOrganization>(e => e.OrganizationId).
                    HasConstraintName("FK_BannedOrganization_Organization");
            });
        }
    }
}
