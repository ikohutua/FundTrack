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
            : base(options)
        {
        }

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
        /// Gets or sets the bank imports.
        /// </summary>
        /// <value>
        /// The bank imports.
        /// </value>
        public DbSet<BankImport> BankImports { get; set; }

        /// <summary>
        /// Gets or sets the bank import details.
        /// </summary>
        /// <value>
        /// The bank import details.
        /// </value>
        public DbSet<BankImportDetail> BankImportDetails { get; set; }

        /// <summary>
        /// Gets or sets the complaints.
        /// </summary>
        /// <value>
        /// The complaints.
        /// </value>
        public DbSet<Complaint> Complaints { get; set; }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Gets or sets the event images.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public DbSet<EventImage> EventImages { get; set; }

        /// <summary>
        /// Gets or sets the goods types.
        /// </summary>
        /// <value>
        /// The goods types.
        /// </value>
        public DbSet<GoodsType> GoodsTypes { get; set; }

        /// <summary>
        /// Gets or sets the goods categorys.
        /// </summary>
        /// <value>
        /// The goods categorys.
        /// </value>
        public DbSet<GoodsCategory> GoodsCategories { get; set; }

        /// <summary>
        /// Gets or sets the offered items.
        /// </summary>
        /// <value>
        /// The offered items.
        /// </value>
        public DbSet<OfferedItem> OfferedItems { get; set; }

        /// <summary>
        /// Gets or sets the requested items.
        /// </summary>
        /// <value>
        /// The requested items.
        /// </value>
        public DbSet<RequestedItem> RequestedItems { get; set; }

        /// <summary>
        /// Banned users
        /// </summary>
        public DbSet<BannedUser> BannedUsers { get; set; }

        /// <summary>
        /// Banned organizations
        /// </summary>
        public DbSet<BannedOrganization> BannedOrganizations { get; set; }

        /// <summary>
        /// Subscribe organization
        /// </summary>
        public DbSet<SubscribeOrganization> SubscribeOrganizations { get; set; }

        /// <summary>
        /// PasswordResets of user
        /// </summary>
        public DbSet<PasswordReset> PasswordResets { get; set; }

        /// <summary>
        /// Gets or Sets Statuses
        /// </summary>
        public DbSet<Status> Statuses { get; set; }

        /// <summary>
        /// Gets or Sets Statuses
        /// </summary>
        public DbSet<OfferedItemImage> OfferedItemImages { get; set; }

        /// <summary>
        /// Gets or Sets Statuses
        /// </summary>
        public DbSet<RequestedItemImage> RequestedItemImages { get; set; }

        /// <summary>
        /// Gets or Sets Users
        /// </summary>
        public DbSet<UserResponse> UserResponses { get; set; }

        /// <summary>
        /// Gets or Sets Users
        /// </summary>
        public DbSet<OrganizationResponse> OrganizationResponses { get; set; }

        /// <summary>
        /// Gets or Setsc FinOpImages
        /// </summary>
        public DbSet<FinOpImage> FinOpImages { get; set; }


        /// <summary>
        /// Gets or Sets Donation
        /// </summary>
        public DbSet<Donation> Donations { get; set; }

        /// <summary>
        /// Gets or Sets Banks
        /// </summary>
        public DbSet<Bank> Banks { get; set; }

        /// <summary>
        /// Gets or Sets Auto Import Interval
        /// </summary>
        public DbSet<AutoImportIntervals> AutoImportInterval { get; set; }

        /// <summary>
        /// Configures model creation
        /// </summary>
        /// <param name="modelBuilder">modelBuilder to configure Model Creation</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User.Configure(modelBuilder);
            Phone.Configure(modelBuilder);
            Address.Configure(modelBuilder);
            UserAddress.Configure(modelBuilder);
            Role.Configure(modelBuilder);
            Organization.Configure(modelBuilder);
            OrgAddress.Configure(modelBuilder);
            ExternalContact.Configure(modelBuilder);
            Membership.Configure(modelBuilder);
            Currency.Configure(modelBuilder);
            BankAccount.Configure(modelBuilder);
            OrgAccount.Configure(modelBuilder);
            Balance.Configure(modelBuilder);
            Target.Configure(modelBuilder);
            FinOp.Configure(modelBuilder);
            Tag.Configure(modelBuilder);
            TagFinOp.Configure(modelBuilder);
            Event.Configure(modelBuilder);
            EventImage.Configure(modelBuilder);
            Complaint.Configure(modelBuilder);
            GoodsType.Configure(modelBuilder);
            GoodsCategory.Configure(modelBuilder);
            OfferedItem.Configure(modelBuilder);
            RequestedItem.Configure(modelBuilder);
            BankImport.Configure(modelBuilder);
            BankImportDetail.Configure(modelBuilder);
            PasswordReset.Configure(modelBuilder);
            BannedUser.Configure(modelBuilder);
            BannedOrganization.Configure(modelBuilder);
            SubscribeOrganization.Configure(modelBuilder);
            Status.Configure(modelBuilder);
            OfferedItemImage.Configure(modelBuilder);
            RequestedItemImage.Configure(modelBuilder);
            UserResponse.Configure(modelBuilder);
            OrganizationResponse.Configure(modelBuilder);
            Donation.Configure(modelBuilder);   
            Bank.Configure(modelBuilder);
            AutoImportIntervals.Configure(modelBuilder);
        }
    }
}