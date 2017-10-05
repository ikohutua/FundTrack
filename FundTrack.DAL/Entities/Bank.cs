using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Bank entity
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// Gets or Sets Id of BankAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of the Bank
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Gets or Sets MFO of Bank
        /// </summary>
        public string MFO { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Bank");

                entity.Property(e => e.BankName).IsRequired().HasMaxLength(50);

                entity.Property(e => e.MFO).IsRequired().HasMaxLength(6);
            });
        }

        public static implicit operator BankViewModel(Bank item)
        {
            return new BankViewModel
            {
                Id = item.Id,
                BankName = item.BankName,
                MFO = item.MFO
            };
        }

        public static implicit operator Bank(BankViewModel model)
        {
            return new Bank
            {
                Id = model.Id,
                BankName = model.BankName,
                MFO = model.MFO
            };
        }
    }
}
