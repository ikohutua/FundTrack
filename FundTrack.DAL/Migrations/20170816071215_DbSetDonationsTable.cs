using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class DbSetDonationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_BankAccounts_BankAccountId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Currencies_CurrencyId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Targets_TargetId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Users_UserId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_Donation_DonationId",
                table: "FinOps");

            migrationBuilder.RenameTable(
                name: "Donation",
                newName: "Donations");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_UserId",
                table: "Donations",
                newName: "IX_Donations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_TargetId",
                table: "Donations",
                newName: "IX_Donations_TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_CurrencyId",
                table: "Donations",
                newName: "IX_Donations_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_BankAccountId",
                table: "Donations",
                newName: "IX_Donations_BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_BankAccounts_BankAccountId",
                table: "Donations",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Currencies_CurrencyId",
                table: "Donations",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Targets_TargetId",
                table: "Donations",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Users_UserId",
                table: "Donations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_Donations_DonationId",
                table: "FinOps",
                column: "DonationId",
                principalTable: "Donations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_BankAccounts_BankAccountId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Currencies_CurrencyId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Targets_TargetId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Users_UserId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_Donations_DonationId",
                table: "FinOps");

            migrationBuilder.RenameTable(
                name: "Donations",
                newName: "Donation");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_UserId",
                table: "Donation",
                newName: "IX_Donation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_TargetId",
                table: "Donation",
                newName: "IX_Donation_TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_CurrencyId",
                table: "Donation",
                newName: "IX_Donation_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donation",
                newName: "IX_Donation_BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_BankAccounts_BankAccountId",
                table: "Donation",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Currencies_CurrencyId",
                table: "Donation",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Targets_TargetId",
                table: "Donation",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Users_UserId",
                table: "Donation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_Donation_DonationId",
                table: "FinOps",
                column: "DonationId",
                principalTable: "Donation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
