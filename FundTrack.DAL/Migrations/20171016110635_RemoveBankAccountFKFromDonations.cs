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

            migrationBuilder.RenameColumn(
                name: "BankAccountId",
                newName: "OrgAccountId",
                table: "Donations");

            migrationBuilder.AlterColumn<int>(
                name: "OrgAccountId",
                table: "Donations",
                nullable: true,
                oldClrType: typeof(int));   
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrgAccountId",
                table: "Donations",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.RenameColumn(
                name: "OrgAccountId",
                newName: "BankAccountId",
                table: "Donations");

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
