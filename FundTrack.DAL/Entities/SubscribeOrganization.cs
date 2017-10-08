using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    public class SubscribeOrganization
    {
        /// <summary>
        /// Subscribe organization id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Organization Id
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property user
        /// </summary>
        public virtual User User { get; set; }
        
        /// <summary>
        /// Navigation property organization
        /// </summary>
        public virtual Organization Organization {get; set;}

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscribeOrganization>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_SubscribeOrganization");
                entity.HasOne(e => e.User).
                    WithMany(e => e.SubscribeOrganization).
                    HasForeignKey(e => e.UserId).
                    HasConstraintName("FK_SubscribeOrganization_User");
                entity.HasOne(e => e.Organization).
                    WithMany(e => e.SubscribeOrganization).
                    HasForeignKey(e => e.OrganizationId).
                    HasConstraintName("FK_SubscribeOrganization_Organization");
            });
        }
    }
}
