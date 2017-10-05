using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Tag entity
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or Sets Id of Tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Tag
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or Sets TagFinOp navigation property
        /// </summary>
        public virtual ICollection<TagFinOp> TagFinOps { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Tag");

                entity.Property(e => e.TagName).IsRequired().HasMaxLength(30);
            });
        }
    }
}
