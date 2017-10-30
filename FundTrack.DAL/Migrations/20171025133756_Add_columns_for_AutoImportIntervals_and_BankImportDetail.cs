using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Add_columns_for_AutoImportIntervals_and_BankImportDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinOpId",
                table: "BankImportDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "AutoImportInterval",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankImportDetails_FinOpId",
                table: "BankImportDetails",
                column: "FinOpId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankImportDetails_FinOp",
                table: "BankImportDetails",
                column: "FinOpId",
                principalTable: "FinOps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankImportDetails_FinOp",
                table: "BankImportDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankImportDetails_FinOpId",
                table: "BankImportDetails");

            migrationBuilder.DropColumn(
                name: "FinOpId",
                table: "BankImportDetails");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "AutoImportInterval");
        }
    }
}
