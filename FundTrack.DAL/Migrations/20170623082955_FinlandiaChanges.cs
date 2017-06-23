using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FundTrack.DAL.Migrations
{
    public partial class FinlandiaChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Membershipes_UserId",
                table: "Membershipes");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SaltKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Organizations");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Offers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BannedOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannedOrganization_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannedUser_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscribeOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscribeOrganization_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscribeOrganization_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Membershipes_UserId",
                table: "Membershipes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannedOrganizations_OrganizationId",
                table: "BannedOrganizations",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannedUsers_UserId",
                table: "BannedUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscribeOrganizations_OrganizationId",
                table: "SubscribeOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribeOrganizations_UserId",
                table: "SubscribeOrganizations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedOrganizations");

            migrationBuilder.DropTable(
                name: "BannedUsers");

            migrationBuilder.DropTable(
                name: "SubscribeOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_Membershipes_UserId",
                table: "Membershipes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SaltKey",
                table: "Users",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Organizations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Membershipes_UserId",
                table: "Membershipes",
                column: "UserId");
        }
    }
}
