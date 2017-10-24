using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Set_BankIdFK_To_Not_Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Bank",
                table: "BankAccounts"
            );

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts"
                );

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                table: "BankAccounts",
                nullable: false
                );

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Bank",
                table: "BankAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Bank",
                table: "BankAccounts"
            );

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts"
            );

            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                table: "BankAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Bank",
                table: "BankAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id"
            );
        }
    }
}
