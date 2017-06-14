using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FundTrack.DAL.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    Street = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 30, nullable: false),
                    ShortName = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TagName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TargetName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Target", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    FB_Link = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true),
                    SaltKey = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccNumber = table.Column<string>(maxLength: 20, nullable: false),
                    BankName = table.Column<string>(maxLength: 50, nullable: false),
                    EDRPOU = table.Column<string>(maxLength: 10, nullable: false),
                    MFO = table.Column<string>(maxLength: 6, nullable: false),
                    OrgId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_Organization",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrgAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    OrgId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgAddress_Address",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgAddress_Organization",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceLogin = table.Column<string>(maxLength: 100, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalContact_ExternalService",
                        column: x => x.ServiceId,
                        principalTable: "ExternalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalContact_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membershipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrgId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_Organization",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membership_Role",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membership_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(maxLength: 15, nullable: false),
                    PhoneType = table.Column<string>(maxLength: 20, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddress_Address",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddress_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrgAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountType = table.Column<string>(maxLength: 10, nullable: false),
                    BankAccId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OrgAccountName = table.Column<string>(maxLength: 100, nullable: false),
                    OrgId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgAccount_BankAccount",
                        column: x => x.BankAccId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgAccount_Currency",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgAccount_Organization",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    datetime = table.Column<DateTime>(nullable: false),
                    OrgAccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance_OrgAccount",
                        column: x => x.OrgAccountId,
                        principalTable: "OrgAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinOps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccFromId = table.Column<int>(nullable: true),
                    AccToId = table.Column<int>(nullable: true),
                    decimal182 = table.Column<decimal>(name: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FinOpDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TargetId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinOp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinOp_OrgAccountFrom",
                        column: x => x.AccFromId,
                        principalTable: "OrgAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinOp_OrgAccountTo",
                        column: x => x.AccToId,
                        principalTable: "OrgAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinOp_Target",
                        column: x => x.TargetId,
                        principalTable: "Targets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinOp_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagFinOps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FinOpId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagFinOp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagFinOp_FinOp",
                        column: x => x.FinOpId,
                        principalTable: "FinOps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagFinOp_Tag",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balances_OrgAccountId",
                table: "Balances",
                column: "OrgAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_OrgId",
                table: "BankAccounts",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalContacts_ServiceId",
                table: "ExternalContacts",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalContacts_UserId",
                table: "ExternalContacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_AccFromId",
                table: "FinOps",
                column: "AccFromId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_AccToId",
                table: "FinOps",
                column: "AccToId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_TargetId",
                table: "FinOps",
                column: "TargetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_UserId",
                table: "FinOps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Membershipes_OrgId",
                table: "Membershipes",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Membershipes_RoleId",
                table: "Membershipes",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Membershipes_UserId",
                table: "Membershipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgAccounts_BankAccId",
                table: "OrgAccounts",
                column: "BankAccId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgAccounts_CurrencyId",
                table: "OrgAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgAccounts_OrgId",
                table: "OrgAccounts",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgAddresses_AddressId",
                table: "OrgAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgAddresses_OrgId",
                table: "OrgAddresses",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_UserId",
                table: "Phones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagFinOps_FinOpId",
                table: "TagFinOps",
                column: "FinOpId");

            migrationBuilder.CreateIndex(
                name: "IX_TagFinOps_TagId",
                table: "TagFinOps",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_AddressId",
                table: "UserAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "ExternalContacts");

            migrationBuilder.DropTable(
                name: "Membershipes");

            migrationBuilder.DropTable(
                name: "OrgAddresses");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "TagFinOps");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "ExternalServices");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "FinOps");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "OrgAccounts");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
