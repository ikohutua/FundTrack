using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Membership entity
    /// </summary>
    public class Membership
    {
        /// <summary>
        /// Gets or Sets Id of Membership
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Role
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int? OrgId { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets Role navigation property
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Membership");

                entity.HasOne(m => m.User)
                    .WithOne(u => u.Membership)
                    .HasForeignKey<Membership>(m => m.UserId)
                    .HasConstraintName("FK_Membership_User");

                entity.HasOne(m => m.Organization)
                    .WithMany(o => o.Memberships)
                    .HasForeignKey(m => m.OrgId)
                    .HasConstraintName("FK_Membership_Organization");

                entity.HasOne(m => m.Role)
                    .WithMany(r => r.Memberships)
                    .HasForeignKey(m => m.RoleId)
                    .HasConstraintName("FK_Membership_Role");
            });
        }
    }
}
