using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// EF Database context
    /// </summary>
    public class FundTrackContext : DbContext
    {
        public FundTrackContext(DbContextOptions<FundTrackContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or Sets Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or Sets Addresses
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or Sets OrgAddresses
        /// </summary>
        public DbSet<OrgAddress> OrgAddresses { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or Sets Phones
        /// </summary>
        public DbSet<Phone> Phones { get; set; }

        /// <summary>
        /// Gets or Sets ExtContacts
        /// </summary>
        public DbSet<ExternalContact> ExternalContacts { get; set; }

        /// <summary>
        /// Gets or Sets Ext Services
        /// </summary>
        public DbSet<ExternalService> ExternalServices { get; set; }

        /// <summary>
        /// Gets or Sets Organizations
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }

        /// <summary>
        /// Gets or Sets Membershipes
        /// </summary>
        public DbSet<Membership> Membershipes { get; set; }

        /// <summary>
        /// Gets or Sets UserAddresses
        /// </summary>
        public DbSet<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// Gets or Sets BankAccounts
        /// </summary>
        public DbSet<BankAccount> BankAccounts { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccounts
        /// </summary>
        public DbSet<OrgAccount> OrgAccounts { get; set; }

        /// <summary>
        /// Gets or Sets Balances
        /// </summary>
        public DbSet<Balance> Balances { get; set; }

        /// <summary>
        /// Gets or Sets Currencies
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or Sets FinOps
        /// </summary>
        public DbSet<FinOp> FinOps { get; set; }

        /// <summary>
        /// Gets or Sets Tags
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or Sets TagFinOps
        /// </summary>
        public DbSet<TagFinOp> TagFinOps { get; set; }

        /// <summary>
        /// Gets or Sets Targets
        /// </summary>
        public DbSet<Target> Targets { get; set; }

        /// <summary>
        /// Configures model creation
        /// </summary>
        /// <param name="modelBuilder">modelBuilder to configure Model Creation</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_User");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Login).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);

                entity.Property(e => e.SaltKey).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Phone");

                entity.Property(e => e.Number).IsRequired().HasMaxLength(15);

                entity.Property(e => e.PhoneType).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.User)
                      .WithMany(u => u.Phones)
                      .HasForeignKey(p => p.UserId)
                      .HasConstraintName("FK_Phone_User");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Address");

                entity.Property(e => e.City).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Country).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Street).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_UserAddress");

                entity.HasOne(ua => ua.User)
                      .WithMany(u => u.UserAddresses)
                      .HasForeignKey(ua => ua.UserId)
                      .HasConstraintName("FK_UserAddress_User");

                entity.HasOne(ua => ua.Address)
                      .WithMany(a => a.UserAddresses)
                      .HasForeignKey(a => a.AddressId)
                      .HasConstraintName("FK_UserAddress_Address");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Role");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Organization");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<OrgAddress>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_OrgAddress");

                entity.HasOne(oa => oa.Organization)
                      .WithMany(o => o.OrgAddresses)
                      .HasForeignKey(oa => oa.OrgId)
                      .HasConstraintName("FK_OrgAddress_Organization");

                entity.HasOne(oa => oa.Address)
                      .WithMany(a => a.OrgAddresses)
                      .HasForeignKey(oa => oa.AddressId)
                      .HasConstraintName("FK_OrgAddress_Address");
            });

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

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Membership");

                entity.HasOne(m => m.User)
                      .WithMany(u => u.Memberships)
                      .HasForeignKey(m => m.UserId)
                      .HasConstraintName("FK_Membership_User");

                entity.HasOne(m => m.Organization)
                      .WithMany(o => o.Memberships)
                      .HasForeignKey(m => m.OrgId)
                      .HasConstraintName("FK_Membership_Organization");

                entity.HasOne(m => m.Role)
                      .WithMany(r => r.Memberships)
                      .HasForeignKey(m => m.RoleId)
                      .HasConstraintName("FK_Membership_Role");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Currency");

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(5);

                entity.Property(e => e.FullName).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_BankAccount");

                entity.Property(e => e.AccNumber).IsRequired().HasMaxLength(20);

                entity.Property(e => e.MFO).IsRequired().HasMaxLength(6);

                entity.Property(e => e.EDRPOU).IsRequired().HasMaxLength(10);

                entity.Property(e => e.BankName).IsRequired().HasMaxLength(50);

                entity.HasOne(ba => ba.Organization)
                      .WithMany(o => o.BankAccounts)
                      .HasForeignKey(ba => ba.OrgId)
                      .HasConstraintName("FK_BankAccount_Organization");
            });

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

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Balance");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.HasOne(b => b.OrgAccount)
                      .WithMany(oa => oa.Balances)
                      .HasForeignKey(b => b.OrgAccountId)
                      .HasConstraintName("FK_Balance_OrgAccount");
            });

            modelBuilder.Entity<Target>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Target");

                entity.Property(e => e.TargetName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<FinOp>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_FinOp");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.FinOpDate).HasColumnType("datetime");

                entity.HasOne(fo => fo.Target)
                      .WithOne(t => t.FinOp)
                      .HasForeignKey<FinOp>(fo => fo.TargetId)
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
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Tag");

                entity.Property(e => e.TagName).IsRequired().HasMaxLength(30);
            });

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