using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Currency entity
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Gets or Sets Id of Currency
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Currency Short Name
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or Sets Currency Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccounts navigation property
        /// </summary>
        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }

        public virtual ICollection<Donation> Donates { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Currency");

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(5);

                entity.Property(e => e.FullName).IsRequired().HasMaxLength(30);
            });
        }
    }
}
