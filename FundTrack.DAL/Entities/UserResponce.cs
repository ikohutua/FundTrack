using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class UserResponse
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
        public int RequestedItemId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? OfferedItemId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public int? StatusId { get; set; }
        
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
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual RequestedItem RequestedItem { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual OfferedItem OfferedItem { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public virtual Status Status { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserResponse>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_UserResponse");

                entity.Property(e => e.RequestedItemId).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(ur => ur.Status)
                    .WithMany(s => s.UserResponses)
                    .HasForeignKey(ur => ur.StatusId)
                    .HasConstraintName("FK_UserResponse_Status");

                entity.HasOne(ur => ur.RequestedItem)
                    .WithMany(ri => ri.UserResponses)
                    .HasForeignKey(ur => ur.RequestedItemId)
                    .HasConstraintName("FK_UserResponse_RequestedItem");

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserResponses)
                    .HasForeignKey(ur => ur.UserId)
                    .HasConstraintName("FK_UserResponse_User");

                entity.HasOne(e => e.OfferedItem)
                    .WithOne(e => e.UserResponse)
                    .HasForeignKey<UserResponse>(e => e.OfferedItemId)
                    .HasConstraintName("FK_UserResponse_OfferedItem");
            });
        }
    }
}
