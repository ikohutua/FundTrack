using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Phone entity
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Gets or Sets Id of Phone
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Phone Number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or Sets Phone Type
        /// </summary>     
        public string PhoneType { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Phone");

                entity.Property(e => e.Number).IsRequired().HasMaxLength(15);

                entity.Property(e => e.PhoneType).HasMaxLength(20);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Phones)
                    .HasForeignKey(p => p.UserId)
                    .HasConstraintName("FK_Phone_User");
            });
        }
    }
}
