using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class Status
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the status name for request or offer - новий, активний, завершено
        /// </summary>
        /// <value>
        /// The status name
        /// </value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the offered items.
        /// </summary>
        /// <value>
        /// The offered items.
        /// </value>
        public virtual ICollection<OfferedItem> OfferedItems { get; set; }

        /// <summary>
        /// Gets or sets the requested items.
        /// </summary>
        /// <value>
        /// The requested items.
        /// </value>
        public virtual ICollection<RequestedItem> RequestedItems { get; set; }

        /// <summary>
        /// Gets or sets the user responses.
        /// </summary>
        /// <value>
        /// The user responses.
        /// </value>
        public virtual ICollection<UserResponse> UserResponses { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Status");

                entity.Property(e => e.StatusName).IsRequired().HasMaxLength(20);
            });
        }
    }
}

