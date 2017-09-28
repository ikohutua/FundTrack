using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// UserAddress entity
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// Gets or Sets Id of UserAddress
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Address
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets Address navigation property
        /// </summary>
        public virtual Address Address { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_UserAddress");

                entity.HasOne(ua => ua.User)
                    .WithMany(u => u.UserAddresses)
                    .HasForeignKey(ua => ua.UserId)
                    .HasConstraintName("FK_UserAddress_User");

                entity.HasOne(ua => ua.Address)
                    .WithMany(a => a.UserAddresses)
                    .HasForeignKey(a => a.AddressId)
                    .HasConstraintName("FK_UserAddress_Address");
            });
        }
    }
}
