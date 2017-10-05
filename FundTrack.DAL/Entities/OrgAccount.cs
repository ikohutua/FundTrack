using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// OrgAccount entity
    /// </summary>
    public class OrgAccount
    {
        /// <summary>
        /// Gets or Sets Id of OrgAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets Id of BankAccount
        /// </summary>
        public int? BankAccId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Currency
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or Sets Name of OrgAccount
        /// </summary>
        public string OrgAccountName { get; set; }

        /// <summary>
        /// Gets or Sets Type of OrgAccount
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or Sets Description of OrgAccount
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets CurrentBalance on OrgAccount
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Gets or Sets TargetId navigation property
        /// </summary>
        public int? TargetId { get; set; }

        /// <summary>
        /// Gets or Sets UserId navigation property
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or Sets CreationDate navigation property
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Gets or Sets BankAccount navigation property
        /// </summary>
        public virtual BankAccount BankAccount { get; set; }

        /// <summary>
        /// Gets or Sets Currency navigation property
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or Sets Target navigation property
        /// </summary>
        public virtual Target Target { get; set; }

        /// <summary>
        /// Gets or Sets Balance navigation property
        /// </summary>
        public virtual ICollection<Balance> Balances { get; set; }

        /// <summary>
        /// Gets or Sets FinOpFrom navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOpsFrom { get; set; }

        /// <summary>
        /// Gets or Sets FinOpTo navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOpsTo { get; set; }
        #region Operators
        public static implicit operator OrgAccount (OrgAccountViewModel model)
        {
            return new OrgAccount
            {
                Id = model.Id,
                OrgId = model.OrgId,
                BankAccId = model.BankAccId,
                CurrencyId = model.CurrencyId,
                OrgAccountName = model.OrgAccountName,
                AccountType = model.AccountType,
                Description=model.Description,
                CurrentBalance=model.CurrentBalance,
                TargetId = model.TargetId
            };
        }
        public static implicit operator OrgAccountViewModel (OrgAccount item)
        {
            return new OrgAccountViewModel
            {
                Id=item.Id,
                OrgId=item.OrgId,
                BankAccId=item.BankAccId,
                CurrencyId=item.CurrencyId,
                OrgAccountName=item.OrgAccountName,
                AccountType=item.AccountType,
                Description=item.Description,
                CurrentBalance=item.CurrentBalance,
                TargetId = item.TargetId
            };
        }
        #endregion

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrgAccount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_OrgAccount");

                entity.Property(e => e.OrgAccountName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.AccountType).IsRequired().HasMaxLength(10);

                entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18,2)");

                entity.HasOne(oa => oa.Organization)
                    .WithMany(o => o.OrgAccounts)
                    .HasForeignKey(oa => oa.OrgId)
                    .HasConstraintName("FK_OrgAccount_Organization");

                entity.HasOne(oa => oa.BankAccount)
                    .WithMany(ba => ba.OrgAccounts)
                    .HasForeignKey(oa => oa.BankAccId)
                    .HasConstraintName("FK_OrgAccount_BankAccount");

                entity.HasOne(oa => oa.Currency)
                    .WithMany(c => c.OrgAccounts)
                    .HasForeignKey(oa => oa.CurrencyId)
                    .HasConstraintName("FK_OrgAccount_Currency");
            });
        }
    }
}
