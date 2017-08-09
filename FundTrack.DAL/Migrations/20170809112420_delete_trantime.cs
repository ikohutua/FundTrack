using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class delete_trantime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trantime",
                table: "BankImportDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsLooked",
                table: "BankImportDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLooked",
                table: "BankImportDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "Trantime",
                table: "BankImportDetails",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
