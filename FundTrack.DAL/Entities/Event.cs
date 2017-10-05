using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class Event
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// EventImages navigation property
        /// </summary>
        public virtual ICollection<EventImage> EventImages { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Event");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.CreateDate).IsRequired().HasColumnType("datetime");

                entity.HasOne(e => e.Organization)
                    .WithMany(o => o.Events)
                    .HasForeignKey(e => e.OrganizationId)
                    .HasConstraintName("FK_Event_Organization");
            });
        }
    }
}
