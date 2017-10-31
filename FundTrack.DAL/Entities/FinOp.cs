using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// FinOp entity
    /// </summary>
    public class FinOp
    {
        /// <summary>
        /// Gets or Sets Id of FinOp
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Gets or Sets Id of Target
        /// </summary>
        public int? TargetId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Account that made the FinOp
        /// </summary>
        public int? AccFromId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Account that reseived the FinOp
        /// </summary>
        public int? AccToId { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Donation
        /// </summary>
        public int? DonationId { get; set; }

        /// <summary>
        /// Gets or Sets Amount of money in FinOp 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or Sets Description of FinOp
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets type of FinOp
        /// </summary>
        public int FinOpType { get; set; }

        /// <summary>
        /// Gets or Sets Date of FinOp
        /// </summary>
        public DateTime FinOpDate { get; set; }

        /// <summary>
        /// Gets or Sets Target navigation property
        /// </summary>
        public virtual Target Target { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccountFrom navigation property
        /// </summary>
        public virtual OrgAccount OrgAccountFrom { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccountTo navigation property
        /// </summary>
        public virtual OrgAccount OrgAccountTo { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets TagFinOp navigation property
        /// </summary>
        public virtual ICollection<TagFinOp> TagFinOps { get; set; }
        
        /// <summary>
        /// Gets or Sets Donation navigation property
        /// </summary>
        public virtual Donation Donation { get; set; }

        public virtual ICollection<BankImportDetail> BankImportDetails { get; set; }

        /// <summary>
        /// Gets or Sets table for binding
        /// </summary>
        public virtual ICollection<FinOpImage> FinOpImage { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinOp>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_FinOp");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.FinOpDate).HasColumnType("datetime");

                entity.HasOne(fo => fo.Target)
                    .WithMany(t => t.FinOp)
                    .HasForeignKey(fo => fo.TargetId)
                    .HasConstraintName("FK_FinOp_Target");

                entity.HasOne(fo => fo.OrgAccountFrom)
                    .WithMany(oa => oa.FinOpsFrom)
                    .HasForeignKey(fo => fo.AccFromId)
                    .HasConstraintName("FK_FinOp_OrgAccountFrom");

                entity.HasOne(fo => fo.OrgAccountTo)
                    .WithMany(oa => oa.FinOpsTo)
                    .HasForeignKey(fo => fo.AccToId)
                    .HasConstraintName("FK_FinOp_OrgAccountTo");

                entity.HasOne(fo => fo.User)
                    .WithMany(u => u.FinOps)
                    .HasForeignKey(fo => fo.UserId)
                    .HasConstraintName("FK_FinOp_User");
                entity.HasOne(fo => fo.Donation)
                    .WithOne();
            });
        }
    }
}
