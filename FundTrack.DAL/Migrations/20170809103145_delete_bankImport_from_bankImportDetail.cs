using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class delete_bankImport_from_bankImportDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankImportDetails_BankImport",
                table: "BankImportDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankImportDetails_BankImportId",
                table: "BankImportDetails");

            migrationBuilder.DropColumn(
                name: "BankImportId",
                table: "BankImportDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankImportId",
                table: "BankImportDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BankImportDetails_BankImportId",
                table: "BankImportDetails",
                column: "BankImportId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankImportDetails_BankImport",
                table: "BankImportDetails",
                column: "BankImportId",
                principalTable: "BankImports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
