using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// TagFinOp entity
    /// </summary>
    public class TagFinOp
    {
        /// <summary>
        /// Gets or Sets Id of TagFinOp
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Tag
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Gets or Sets Id of FinOp
        /// </summary>
        public int FinOpId { get; set; }

        /// <summary>
        /// Gets or Sets Tag navigation property
        /// </summary>
        public virtual Tag Tag { get; set; }

        /// <summary>
        /// Gets or Sets FinOp navigation property
        /// </summary>
        public virtual FinOp FinOp { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagFinOp>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_TagFinOp");

                entity.HasOne(tfp => tfp.Tag)
                    .WithMany(t => t.TagFinOps)
                    .HasForeignKey(tfp => tfp.TagId)
                    .HasConstraintName("FK_TagFinOp_Tag");

                entity.HasOne(tfp => tfp.FinOp)
                    .WithMany(fo => fo.TagFinOps)
                    .HasForeignKey(tfp => tfp.FinOpId)
                    .HasConstraintName("FK_TagFinOp_FinOp");
            });
        }
    }
}
