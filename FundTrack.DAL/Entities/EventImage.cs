using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class EventImage
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
        public int EventId { get; set; }

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
        public virtual Event Event { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_EventImage");

                entity.Property(e => e.EventId).IsRequired();

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.HasOne(e => e.Event)
                    .WithMany(o => o.EventImages)
                    .HasForeignKey(e => e.EventId)
                    .HasConstraintName("FK_EvantImage_Event");
            });
        }
    }
}
