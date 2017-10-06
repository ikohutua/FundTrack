using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Role entity
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or Sets Id of Role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Role Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Membership navigation property
        /// </summary>
        public virtual ICollection<Membership> Memberships { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Role");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(20);
            });
        }
    }
}
