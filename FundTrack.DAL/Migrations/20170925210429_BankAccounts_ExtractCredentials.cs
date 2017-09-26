using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class BankAccounts_ExtractCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtractMerchantId",
                table: "BankAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtractMerchantPassword",
                table: "BankAccounts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExtractEnabled",
                table: "BankAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtractMerchantId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "ExtractMerchantPassword",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "IsExtractEnabled",
                table: "BankAccounts");
        }
    }
}
