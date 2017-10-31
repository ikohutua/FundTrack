using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// BankAccount entity
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Gets or Sets Id of BankAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets AccNumber of BankAccount
        /// </summary>
        public string AccNumber { get; set; }
        /// <summary>
        /// Gets or sets card number property
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// Gets or Sets MFO of BankAccount
        /// </summary>
        public string MFO { get; set; }

        /// <summary>
        /// Gets or Sets EDRPOU of BankAccount
        /// </summary>
        public string EDRPOU { get; set; }

        /// <summary>
        /// Gets or Sets Name of the Bank
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Gets or sets id of the merchant of the payment system
        /// </summary>
        public int? MerchantId { get; set; }
        /// <summary>
        /// Gets or sets password of the merchant of the payment system
        /// </summary>
        public string MerchantPassword { get; set; }
        /// <summary>
        /// Gets or sets bit that determines if donation to account is enabled
        /// </summary>
        public bool? IsDonationEnabled { get; set; }

        /// <summary>
        /// Gets or sets id of the merchant of the bank system for getting extracts
        /// </summary>
        public int? ExtractMerchantId { get; set; }
        /// <summary>
        /// Gets or sets password of the merchant of the bank system for getting extracts
        /// </summary>
        public string ExtractMerchantPassword { get; set; }
        /// <summary>
        /// Gets or sets bit that determines if getting of extracts from account are enabled
        /// </summary>
        public bool? IsExtractEnabled { get; set; }

        /// <summary>
        /// Gets or Sets bank id
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccount navigation property
        /// </summary>
        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }

        /// <summar>
        /// Gets or Sets navigation property
        /// </summar>
        public virtual Bank Bank { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_BankAccount");

                entity.Property(e => e.AccNumber).HasMaxLength(20);

                entity.Property(e => e.MFO).HasMaxLength(6);

                entity.Property(e => e.EDRPOU).HasMaxLength(10);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.CardNumber).HasMaxLength(16);

                entity.Property(e => e.BankId).IsRequired();

                entity.HasOne(ba => ba.Organization)
                    .WithMany(o => o.BankAccounts)
                    .HasForeignKey(ba => ba.OrgId)
                    .HasConstraintName("FK_BankAccount_Organization");

                entity.HasOne(ba => ba.Bank)
                    .WithMany(b => b.BankAccounts)
                    .HasForeignKey(ba => ba.BankId)
                    .HasConstraintName("FK_BankAccount_Bank");
            });
        }
    }
}
