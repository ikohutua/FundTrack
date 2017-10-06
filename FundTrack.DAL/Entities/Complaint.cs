using System;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class Complaint
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

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
        /// Gets or sets a value indicating whether this instance is looked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is looked; otherwise, <c>false</c>.
        /// </value>
        public bool IsLooked { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public virtual Organization Organization { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Complaint");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.CreateDate).IsRequired().HasColumnType("datetime");

                entity.Property(e => e.IsLooked).IsRequired();

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Complaints)
                    .HasForeignKey(c => c.UserId)
                    .HasConstraintName("FK_Complaint_User");

                entity.HasOne(c => c.Organization)
                    .WithMany(o => o.Complaints)
                    .HasForeignKey(c => c.OrganizationId)
                    .HasConstraintName("FK_Complaint_Organization");
            });
        }
    }
}
