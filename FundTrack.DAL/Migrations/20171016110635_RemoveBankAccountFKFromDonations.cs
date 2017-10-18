using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace FundTrack.DAL.Migrations
{
    public partial class RemoveBankAccountFKFromDonations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_BankAccount",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "Donations");

            migrationBuilder.AddColumn<int>(
                name: "OrgAccountId",
                table: "Donations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgAccountId",
                table: "Donations");

            migrationBuilder.AddColumn<int>(
                name: "BankAccountId",
                table: "Donations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donations",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_BankAccount",
                table: "Donations",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
