using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FundTrack.DAL.Concrete;

namespace FundTrack.DAL.Migrations
{
    [DbContext(typeof(FundTrackContext))]
    partial class FundTrackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FundTrack.DAL.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("PK_Address");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.AutoImportIntervals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Interval");

                    b.Property<int>("OrgId");

                    b.HasKey("Id")
                        .HasName("PK_AutoImportIntervals");

                    b.HasIndex("OrgId")
                        .IsUnique();

                    b.ToTable("AutoImportInterval");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BalanceDate")
                        .HasColumnType("datetime");

                    b.Property<int>("OrgAccountId");

                    b.HasKey("Id")
                        .HasName("PK_Balance");

                    b.HasIndex("OrgAccountId");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MFO")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("Id")
                        .HasName("PK_Bank");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccNumber")
                        .HasMaxLength(20);

                    b.Property<int>("BankId");

                    b.Property<string>("BankName")
                        .HasMaxLength(50);

                    b.Property<string>("CardNumber")
                        .HasMaxLength(16);

                    b.Property<string>("EDRPOU")
                        .HasMaxLength(10);

                    b.Property<int?>("ExtractMerchantId");

                    b.Property<string>("ExtractMerchantPassword");

                    b.Property<bool?>("IsDonationEnabled");

                    b.Property<bool?>("IsExtractEnabled");

                    b.Property<string>("MFO")
                        .HasMaxLength(6);

                    b.Property<int?>("MerchantId");

                    b.Property<string>("MerchantPassword");

                    b.Property<int>("OrgId");

                    b.HasKey("Id")
                        .HasName("PK_BankAccount");

                    b.HasIndex("BankId");

                    b.HasIndex("OrgId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BankImport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Debet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdMerchant");

                    b.Property<string>("Signature")
                        .IsRequired();

                    b.HasKey("Id")
                        .HasName("PK_BankImport");

                    b.ToTable("BankImports");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BankImportDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Amount")
                        .IsRequired();

                    b.Property<int?>("AppCode")
                        .HasMaxLength(8);

                    b.Property<string>("Card")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("CardAmount")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsLooked");

                    b.Property<string>("Rest")
                        .IsRequired();

                    b.Property<string>("Terminal")
                        .IsRequired();

                    b.Property<DateTime>("Trandate")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK_BankImportDetail");

                    b.ToTable("BankImportDetails");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BannedOrganization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id")
                        .HasName("PK_BannedOrganization");

                    b.HasIndex("OrganizationId")
                        .IsUnique();

                    b.ToTable("BannedOrganizations");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BannedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_BannedUser");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("BannedUsers");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Complaint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsLooked");

                    b.Property<int>("OrganizationId");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_Complaint");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("Id")
                        .HasName("PK_Currency");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("CurrencyId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("DonationDate");

                    b.Property<string>("DonatorEmail");

                    b.Property<Guid>("OrderId");

                    b.Property<int>("OrgAccountId");

                    b.Property<int?>("TargetId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_DonationId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("OrgAccountId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id")
                        .HasName("PK_Event");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.EventImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<bool>("IsMain");

                    b.HasKey("Id")
                        .HasName("PK_EventImage");

                    b.HasIndex("EventId");

                    b.ToTable("EventImages");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.ExternalContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ServiceId");

                    b.Property<string>("ServiceLogin")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_ExternalContact");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("ExternalContacts");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.ExternalService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ExternalServices");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.FinOp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccFromId");

                    b.Property<int?>("AccToId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description");

                    b.Property<int?>("DonationId");

                    b.Property<DateTime>("FinOpDate")
                        .HasColumnType("datetime");

                    b.Property<int>("FinOpType");

                    b.Property<int?>("TargetId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_FinOp");

                    b.HasIndex("AccFromId");

                    b.HasIndex("AccToId");

                    b.HasIndex("DonationId")
                        .IsUnique();

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("FinOps");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.FinOpImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FinOpId");

                    b.Property<string>("ImageUrl");

                    b.HasKey("Id");

                    b.HasIndex("FinOpId");

                    b.ToTable("FinOpImages");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.GoodsCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GoodsTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("PK_GoodsCategory");

                    b.HasIndex("GoodsTypeId");

                    b.ToTable("GoodsCategories");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.GoodsType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("PK_GoodsType");

                    b.ToTable("GoodsTypes");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OrgId");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_Membership");

                    b.HasIndex("OrgId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Membershipes");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OfferedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("GoodsCategoryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("StatusId");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_OfferedItem");

                    b.HasIndex("GoodsCategoryId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("OfferedItems");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OfferedItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<bool>("IsMain");

                    b.Property<int>("OfferedItemId");

                    b.HasKey("Id")
                        .HasName("PK_OfferedItemImage");

                    b.HasIndex("OfferedItemId");

                    b.ToTable("OfferedItemImages");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrgAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("BankAccId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CurrencyId");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description");

                    b.Property<string>("OrgAccountName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("OrgId");

                    b.Property<int?>("TargetId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_OrgAccount");

                    b.HasIndex("BankAccId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("OrgId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("OrgAccounts");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrgAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("OrgId");

                    b.HasKey("Id")
                        .HasName("PK_OrgAddress");

                    b.HasIndex("AddressId");

                    b.HasIndex("OrgId");

                    b.ToTable("OrgAddresses");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("PK_Organization");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrganizationResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("OfferedItemId");

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id")
                        .HasName("PK_OrganizationResponse");

                    b.HasIndex("OfferedItemId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationResponses");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.PasswordReset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("GUID")
                        .IsRequired();

                    b.Property<int>("UserID");

                    b.HasKey("Id")
                        .HasName("PK_PasswordReset");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("PasswordResets");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("PhoneType")
                        .HasMaxLength(20);

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_Phone");

                    b.HasIndex("UserId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.RequestedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("GoodsCategoryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("OrganizationId");

                    b.Property<int>("StatusId");

                    b.HasKey("Id")
                        .HasName("PK_RequestedItem");

                    b.HasIndex("GoodsCategoryId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("StatusId");

                    b.ToTable("RequestedItems");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.RequestedItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<bool>("IsMain");

                    b.Property<int>("RequestedItemId");

                    b.HasKey("Id")
                        .HasName("PK_<RequestedItemImage");

                    b.HasIndex("RequestedItemId");

                    b.ToTable("RequestedItemImages");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_Role");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_Status");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.SubscribeOrganization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrganizationId");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_SubscribeOrganization");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("SubscribeOrganizations");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id")
                        .HasName("PK_Tag");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.TagFinOp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FinOpId");

                    b.Property<int>("TagId");

                    b.HasKey("Id")
                        .HasName("PK_TagFinOp");

                    b.HasIndex("FinOpId");

                    b.HasIndex("TagId");

                    b.ToTable("TagFinOps");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrganizationId");

                    b.Property<int?>("ParentTargetId");

                    b.Property<string>("TargetName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("PK_Target");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("ParentTargetId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FB_Link");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhotoUrl")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("https://s3.eu-central-1.amazonaws.com/fundtrack/default-user-image.png");

                    b.HasKey("Id")
                        .HasName("PK_User");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_UserAddress");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.UserResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int?>("OfferedItemId");

                    b.Property<int>("RequestedItemId");

                    b.Property<int?>("StatusId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_UserResponse");

                    b.HasIndex("OfferedItemId")
                        .IsUnique();

                    b.HasIndex("RequestedItemId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("UserResponses");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.AutoImportIntervals", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithOne("AutoImportInterval")
                        .HasForeignKey("FundTrack.DAL.Entities.AutoImportIntervals", "OrgId")
                        .HasConstraintName("FK_AutoImportIntervals_Organization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Balance", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.OrgAccount", "OrgAccount")
                        .WithMany("Balances")
                        .HasForeignKey("OrgAccountId")
                        .HasConstraintName("FK_Balance_OrgAccount")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BankAccount", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Bank", "Bank")
                        .WithMany("BankAccounts")
                        .HasForeignKey("BankId")
                        .HasConstraintName("FK_BankAccount_Bank")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("BankAccounts")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_BankAccount_Organization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BannedOrganization", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithOne("BannedOrganization")
                        .HasForeignKey("FundTrack.DAL.Entities.BannedOrganization", "OrganizationId")
                        .HasConstraintName("FK_BannedOrganization_Organization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BannedUser", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithOne("BannedUser")
                        .HasForeignKey("FundTrack.DAL.Entities.BannedUser", "UserId")
                        .HasConstraintName("FK_BannedUser_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Complaint", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("Complaints")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK_Complaint_Organization")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("Complaints")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Complaint_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Donation", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Currency", "Currency")
                        .WithMany("Donates")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("FK_Donation_Currency")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.OrgAccount", "OrgAccount")
                        .WithMany()
                        .HasForeignKey("OrgAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Target", "Target")
                        .WithMany("Donates")
                        .HasForeignKey("TargetId")
                        .HasConstraintName("FK_Donation_Target");

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("UserDonations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Donation_User");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Event", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("Events")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK_Event_Organization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.EventImage", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Event", "Event")
                        .WithMany("EventImages")
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK_EvantImage_Event")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.ExternalContact", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.ExternalService", "ExternalService")
                        .WithMany("ExtContacts")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK_ExternalContact_ExternalService")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("ExternalContacts")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ExternalContact_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.FinOp", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.OrgAccount", "OrgAccountFrom")
                        .WithMany("FinOpsFrom")
                        .HasForeignKey("AccFromId")
                        .HasConstraintName("FK_FinOp_OrgAccountFrom");

                    b.HasOne("FundTrack.DAL.Entities.OrgAccount", "OrgAccountTo")
                        .WithMany("FinOpsTo")
                        .HasForeignKey("AccToId")
                        .HasConstraintName("FK_FinOp_OrgAccountTo");

                    b.HasOne("FundTrack.DAL.Entities.Donation", "Donation")
                        .WithOne()
                        .HasForeignKey("FundTrack.DAL.Entities.FinOp", "DonationId");

                    b.HasOne("FundTrack.DAL.Entities.Target", "Target")
                        .WithMany("FinOp")
                        .HasForeignKey("TargetId")
                        .HasConstraintName("FK_FinOp_Target");

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("FinOps")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_FinOp_User");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.FinOpImage", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.FinOp", "FinOp")
                        .WithMany("FinOpImage")
                        .HasForeignKey("FinOpId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.GoodsCategory", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.GoodsType", "GoodsType")
                        .WithMany("GoodsCategories")
                        .HasForeignKey("GoodsTypeId")
                        .HasConstraintName("FK_GoodsCategory_GoodsType")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Membership", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("Memberships")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_Membership_Organization");

                    b.HasOne("FundTrack.DAL.Entities.Role", "Role")
                        .WithMany("Memberships")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Membership_Role")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithOne("Membership")
                        .HasForeignKey("FundTrack.DAL.Entities.Membership", "UserId")
                        .HasConstraintName("FK_Membership_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OfferedItem", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.GoodsCategory", "GoodsCategory")
                        .WithMany("OfferedItems")
                        .HasForeignKey("GoodsCategoryId")
                        .HasConstraintName("FK_OfferedItems_GoodsCategory")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Status", "Status")
                        .WithMany("OfferedItems")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_OfferedItems_Status")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("OfferedItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OfferedItemImage", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.OfferedItem", "OfferedItem")
                        .WithMany("OfferedItemImages")
                        .HasForeignKey("OfferedItemId")
                        .HasConstraintName("FK_OfferedItemImage_OfferedItem")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrgAccount", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.BankAccount", "BankAccount")
                        .WithMany("OrgAccounts")
                        .HasForeignKey("BankAccId")
                        .HasConstraintName("FK_OrgAccount_BankAccount");

                    b.HasOne("FundTrack.DAL.Entities.Currency", "Currency")
                        .WithMany("OrgAccounts")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("FK_OrgAccount_Currency")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("OrgAccounts")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_OrgAccount_Organization")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Target", "Target")
                        .WithMany("OrgAccounts")
                        .HasForeignKey("TargetId")
                        .HasConstraintName("FK_OrgAccount_Targets");

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("OrgAccounts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrgAddress", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Address", "Address")
                        .WithMany("OrgAddresses")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_OrgAddress_Address")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("OrgAddresses")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_OrgAddress_Organization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrganizationResponse", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.OfferedItem", "OfferedItem")
                        .WithMany("OrganizationResponses")
                        .HasForeignKey("OfferedItemId")
                        .HasConstraintName("FK_OrganizationResponse_OfferedItem")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("OrganizationResponses")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK_OrganizationResponse_Organization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.PasswordReset", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithOne("PasswordReset")
                        .HasForeignKey("FundTrack.DAL.Entities.PasswordReset", "UserID")
                        .HasConstraintName("FK_PasswordReset_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Phone", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("Phones")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Phone_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.RequestedItem", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.GoodsCategory", "GoodsCategory")
                        .WithMany("RequestedItems")
                        .HasForeignKey("GoodsCategoryId")
                        .HasConstraintName("FK_RequestedItem_GoodsCategory")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("RequestedItems")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Status", "Status")
                        .WithMany("RequestedItems")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_RequestedItem_Status")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.RequestedItemImage", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.RequestedItem", "RequestedItem")
                        .WithMany("RequestedItemImages")
                        .HasForeignKey("RequestedItemId")
                        .HasConstraintName("FK_<RequestedItemImage_<RequestedItem")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.SubscribeOrganization", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("SubscribeOrganization")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("FK_SubscribeOrganization_Organization")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("SubscribeOrganization")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_SubscribeOrganization_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.TagFinOp", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.FinOp", "FinOp")
                        .WithMany("TagFinOps")
                        .HasForeignKey("FinOpId")
                        .HasConstraintName("FK_TagFinOp_FinOp")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Tag", "Tag")
                        .WithMany("TagFinOps")
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_TagFinOp_Tag")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Target", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organizations")
                        .WithMany("Targets")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Target", "ParentTarget")
                        .WithMany()
                        .HasForeignKey("ParentTargetId");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.UserAddress", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Address", "Address")
                        .WithMany("UserAddresses")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_UserAddress_Address")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserAddress_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.UserResponse", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.OfferedItem", "OfferedItem")
                        .WithOne("UserResponse")
                        .HasForeignKey("FundTrack.DAL.Entities.UserResponse", "OfferedItemId")
                        .HasConstraintName("FK_UserResponse_OfferedItem");

                    b.HasOne("FundTrack.DAL.Entities.RequestedItem", "RequestedItem")
                        .WithMany("UserResponses")
                        .HasForeignKey("RequestedItemId")
                        .HasConstraintName("FK_UserResponse_RequestedItem")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Status", "Status")
                        .WithMany("UserResponses")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_UserResponse_Status");

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("UserResponses")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserResponse_User");
                });
        }
    }
}
