using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class FixedDonationTableRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_CurrencyId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_TargetId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_UserId",
                table: "Donations");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donations",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CurrencyId",
                table: "Donations",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_TargetId",
                table: "Donations",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_BankAccount",
                table: "Donations",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Currency",
                table: "Donations",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Target",
                table: "Donations",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_User",
                table: "Donations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_BankAccount",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Currency",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Target",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_User",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_CurrencyId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_TargetId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_UserId",
                table: "Donations");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BankAccountId",
                table: "Donations",
                column: "BankAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CurrencyId",
                table: "Donations",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_TargetId",
                table: "Donations",
                column: "TargetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId",
                unique: true);

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
        }
    }
}
