using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class dbFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank",
                table: "BankAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgAccount_Targets",
                table: "OrgAccounts",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrgAccount_Targets",
                table: "OrgAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
