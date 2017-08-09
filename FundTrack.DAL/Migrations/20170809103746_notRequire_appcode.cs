using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class notRequire_appcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppCode",
                table: "BankImportDetails",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppCode",
                table: "BankImportDetails",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 8,
                oldNullable: true);
        }
    }
}
