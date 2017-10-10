using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class OfferedItem
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

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
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// EventImages navigation property
        /// </summary>
        public virtual ICollection<OfferedItemImage> OfferedItemImages { get; set; }

        /// <summary>
        /// EventImages navigation property
        /// </summary>
        public virtual ICollection<OrganizationResponse> OrganizationResponses { get; set; }
        /// <summary>
        /// UserResponse navigation property
        /// </summary>
        public virtual UserResponse UserResponse { get; set; }

        public static implicit operator OfferedItem(OfferedItemViewModel model)
        {
            return new OfferedItem
            {
                Description = model.Description,
                Name = model.Name,
                UserId = model.UserId
            };
        }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfferedItem>(entity =>
            {
                entity.HasKey(oi => oi.Id).HasName("PK_OfferedItem");

                entity.Property(oi => oi.Name).IsRequired();

                entity.Property(oi => oi.Description).IsRequired();

                entity.Property(oi => oi.StatusId).IsRequired();

                entity.HasOne(oi => oi.GoodsCategory)
                    .WithMany(gc => gc.OfferedItems)
                    .HasForeignKey(oi => oi.GoodsCategoryId)
                    .HasConstraintName("FK_OfferedItems_GoodsCategory");

                entity.HasOne(oi => oi.Status)
                    .WithMany(s => s.OfferedItems)
                    .HasForeignKey(oi => oi.StatusId)
                    .HasConstraintName("FK_OfferedItems_Status");
            });
        }
    }
}
