using System;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// PasswordReset Entity
    /// </summary>
    public class PasswordReset
    {
        /// <summary>
        /// Gets or Sets Id of PasswordReset
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or Sets Globally unique identifier
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Gets or Sets Exxpire date of reset link
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Gets or Sets User Navigation Property
        /// </summary>
        public virtual User User { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PasswordReset>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_PasswordReset");

                entity.Property(e => e.GUID).IsRequired();

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.HasOne(e => e.User)
                    .WithOne(e => e.PasswordReset)
                    .HasForeignKey<PasswordReset>(e => e.UserID)
                    .HasConstraintName("FK_PasswordReset_User");
            });
        }
    }
}
