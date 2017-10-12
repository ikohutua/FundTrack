using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class BannedUser
    {
        /// <summary>
        /// Gets or sets the banned identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the banned user description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Users navigation property
        /// </summary>
        public virtual User User { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BannedUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_BannedUser");
                entity.HasOne(e => e.User).
                    WithOne(e => e.BannedUser).
                    HasForeignKey<BannedUser>(e => e.UserId).
                    HasConstraintName("FK_BannedUser_User");
            });
        }
    }
}
