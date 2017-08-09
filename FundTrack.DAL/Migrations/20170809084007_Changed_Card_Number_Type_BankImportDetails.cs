using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Changed_Card_Number_Type_BankImportDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Card",
                table: "BankImportDetails",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Card",
                table: "BankImportDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);
        }
    }
}
