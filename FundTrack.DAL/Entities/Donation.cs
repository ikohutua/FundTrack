using System;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Donation Entity
    /// </summary>
    public class Donation
    {
        /// <summary>
        /// Gets or Sets Id of Donation entity
        /// </summary>
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int? UserId { get; set; }
        public int CurrencyId { get; set; }
        public int? TargetId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime DonationDate { get; set; }
        public string DonatorEmail { get; set; }
        public virtual Currency Currency {get;set;}
        public virtual User User { get; set; }
        public virtual Target Target { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_DonationId");

                entity.HasOne(e => e.Currency)
                    .WithMany(c => c.Donates)
                    .HasForeignKey(e => e.CurrencyId)
                    .HasConstraintName("FK_Donation_Currency");

                entity.Property(e => e.Amount).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserDonations)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Donation_User");

                entity.HasOne(e => e.Target)
                    .WithMany(t => t.Donates).
                    HasForeignKey(e => e.TargetId).
                    HasConstraintName("FK_Donation_Target");

                entity.HasOne(e => e.BankAccount)
                    .WithMany(b => b.Donations)
                    .HasForeignKey(e => e.BankAccountId)
                    .HasConstraintName("FK_Donation_BankAccount");
            });
        }
    }
}
