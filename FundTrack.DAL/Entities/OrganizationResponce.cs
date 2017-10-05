using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class OrganizationResponse
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
        public int OfferedItemId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
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
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual OfferedItem OfferedItem { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationResponse>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_OrganizationResponse");

                entity.Property(e => e.OfferedItemId).IsRequired();

                entity.Property(e => e.OrganizationId).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(or => or.OfferedItem)
                    .WithMany(oi => oi.OrganizationResponses)
                    .HasForeignKey(or => or.OfferedItemId)
                    .HasConstraintName("FK_OrganizationResponse_OfferedItem");

                entity.HasOne(or => or.Organization)
                    .WithMany(o => o.OrganizationResponses)
                    .HasForeignKey(or => or.OrganizationId)
                    .HasConstraintName("FK_OrganizationResponse_Organization");
            });
        }
    }
}
