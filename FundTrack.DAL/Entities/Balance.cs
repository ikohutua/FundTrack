using System;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Balance entity
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Gets or Sets Id of Balance
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of OrgAccount
        /// </summary>
        public int OrgAccountId { get; set; }

        /// <summary>
        /// Gets or Sets Date of Balance
        /// </summary>
        public DateTime BalanceDate { get; set; }

        /// <summary>
        /// Gets or Sets Amount of money on the balance
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccount navigation property
        /// </summary>
        public virtual OrgAccount OrgAccount { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Balance");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.HasOne(b => b.OrgAccount)
                    .WithMany(oa => oa.Balances)
                    .HasForeignKey(b => b.OrgAccountId)
                    .HasConstraintName("FK_Balance_OrgAccount");
            });
        }
    }
}
