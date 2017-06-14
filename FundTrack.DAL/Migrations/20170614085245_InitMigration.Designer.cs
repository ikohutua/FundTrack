using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FundTrack.DAL.Concrete;

namespace FundTrack.DAL.Migrations
{
    [DbContext(typeof(FundTrackContext))]
    [Migration("20170614085245_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FundTrack.DAL.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("FundTrack.DAL.Entities.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BalanceDate")
                        .HasColumnName("datetime");

                    b.Property<int>("OrgAccountId");

                    b.HasKey("Id")
                        .HasName("PK_Balance");

                    b.HasIndex("OrgAccountId");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("EDRPOU")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("MFO")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<int>("OrgId");

                    b.HasKey("Id")
                        .HasName("PK_BankAccount");

                    b.HasIndex("OrgId");

                    b.ToTable("BankAccounts");
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
                        .HasColumnName("decimal(18,2)");

                    b.Property<string>("Description");

                    b.Property<DateTime>("FinOpDate")
                        .HasColumnType("datetime");

                    b.Property<int>("TargetId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_FinOp");

                    b.HasIndex("AccFromId");

                    b.HasIndex("AccToId");

                    b.HasIndex("TargetId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("FinOps");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrgId");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_Membership");

                    b.HasIndex("OrgId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Membershipes");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.OrgAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("BankAccId");

                    b.Property<int>("CurrencyId");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description");

                    b.Property<string>("OrgAccountName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("OrgId");

                    b.HasKey("Id")
                        .HasName("PK_OrgAccount");

                    b.HasIndex("BankAccId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("OrgId");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("PK_Organization");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("PhoneType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_Phone");

                    b.HasIndex("UserId");

                    b.ToTable("Phones");
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

                    b.Property<string>("TargetName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("PK_Target");

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

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("SaltKey")
                        .IsRequired()
                        .HasMaxLength(10);

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
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("BankAccounts")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_BankAccount_Organization")
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

                    b.HasOne("FundTrack.DAL.Entities.Target", "Target")
                        .WithOne("FinOp")
                        .HasForeignKey("FundTrack.DAL.Entities.FinOp", "TargetId")
                        .HasConstraintName("FK_FinOp_Target")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("FinOps")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_FinOp_User");
                });

            modelBuilder.Entity("FundTrack.DAL.Entities.Membership", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.Organization", "Organization")
                        .WithMany("Memberships")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("FK_Membership_Organization")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.Role", "Role")
                        .WithMany("Memberships")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Membership_Role")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Membership_User")
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

            modelBuilder.Entity("FundTrack.DAL.Entities.Phone", b =>
                {
                    b.HasOne("FundTrack.DAL.Entities.User", "User")
                        .WithMany("Phones")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Phone_User")
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
        }
    }
}
