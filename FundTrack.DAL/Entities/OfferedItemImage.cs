using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class OfferedItemImage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int OfferedItemId { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets if the image is main image of event.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public bool IsMain { get; set; }

        /// <summary>
        /// Event navigation property
        /// </summary>
        public virtual OfferedItem OfferedItem { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfferedItemImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_OfferedItemImage");

                entity.Property(e => e.OfferedItemId).IsRequired();

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.HasOne(oi => oi.OfferedItem)
                    .WithMany(oii => oii.OfferedItemImages)
                    .HasForeignKey(oii => oii.OfferedItemId)
                    .HasConstraintName("FK_OfferedItemImage_OfferedItem");
            });
        }
    }
}
