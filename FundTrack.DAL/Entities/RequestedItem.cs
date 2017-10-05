using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class RequestedItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the goods category identifier.
        /// </summary>
        /// <value>
        /// The goods category identifier.
        /// </value>
        public int GoodsCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is actual.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is actual; otherwise, <c>false</c>.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the goods category.
        /// </summary>
        /// <value>
        /// The goods category.
        /// </value>
        public virtual GoodsCategory GoodsCategory { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public virtual Status Status { get; set; }

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
        public virtual ICollection<RequestedItemImage> RequestedItemImages { get; set; }

        /// <summary>
        /// UserResponses navigation property
        /// </summary>
        public virtual ICollection<UserResponse> UserResponses { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestedItem>(entity =>
            {
                entity.HasKey(ri => ri.Id).HasName("PK_RequestedItem");

                entity.Property(ri => ri.Name).IsRequired();

                entity.Property(ri => ri.Description).IsRequired();

                entity.Property(ri => ri.StatusId).IsRequired();

                entity.HasOne(ri => ri.GoodsCategory)
                    .WithMany(gc => gc.RequestedItems)
                    .HasForeignKey(ri => ri.GoodsCategoryId)
                    .HasConstraintName("FK_RequestedItem_GoodsCategory");

                entity.HasOne(ri => ri.Status)
                    .WithMany(s => s.RequestedItems)
                    .HasForeignKey(ri => ri.StatusId)
                    .HasConstraintName("FK_RequestedItem_Status");
            });
        }
    }
}
