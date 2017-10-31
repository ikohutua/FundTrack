using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Entities
{
    public class AutoImportIntervals
    {
        /// <summary>
        /// Gets or Sets Id of interval
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets interval in minutes
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or Sets id of organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets date of last bank update
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoImportIntervals>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_AutoImportIntervals");

                entity.Property(e => e.Interval).IsRequired();

                entity.Property(e => e.OrgId).IsRequired();

                entity.HasIndex(e => e.OrgId).IsUnique();

                entity.HasOne(e => e.Organization)
                .WithOne(e => e.AutoImportInterval)
                .HasForeignKey<AutoImportIntervals>(e => e.OrgId)
                .HasConstraintName("FK_AutoImportIntervals_Organization");
            });
        }
    }
}
