using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class AddFKDonationsToOrgAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrgAccountId",
                table: "Donations",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Donations_OrgAccountId",
                table: "Donations",
                column: "OrgAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_OrgAccounts_OrgAccountId",
                table: "Donations",
                column: "OrgAccountId",
                principalTable: "OrgAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_OrgAccounts_OrgAccountId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_OrgAccountId",
                table: "Donations");

            migrationBuilder.AlterColumn<int>(
                name: "OrgAccountId",
                table: "Donations",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
