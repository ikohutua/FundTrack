using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// ExternalContact entity
    /// </summary>
    public class ExternalContact
    {
        /// <summary>
        /// Gets or Sets Id of ExternalContact
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of ExternalService
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Service Login
        /// </summary>    
        public string ServiceLogin { get; set; }

        /// <summary>
        /// Gets or Sets ExternalService navigation property
        /// </summary>
        public virtual ExternalService ExternalService { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExternalContact>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ExternalContact");

                entity.Property(e => e.ServiceLogin).IsRequired().HasMaxLength(100);

                entity.HasOne(ec => ec.User)
                    .WithMany(u => u.ExternalContacts)
                    .HasForeignKey(ec => ec.UserId)
                    .HasConstraintName("FK_ExternalContact_User");

                entity.HasOne(ec => ec.ExternalService)
                    .WithMany(es => es.ExtContacts)
                    .HasForeignKey(ec => ec.ServiceId)
                    .HasConstraintName("FK_ExternalContact_ExternalService");
            });
        }
    }
}
