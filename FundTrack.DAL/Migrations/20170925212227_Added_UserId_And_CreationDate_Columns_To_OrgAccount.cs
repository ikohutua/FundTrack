using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Added_UserId_And_CreationDate_Columns_To_OrgAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "OrgAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrgAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrgAccounts_UserId",
                table: "OrgAccounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrgAccounts_Users_UserId",
                table: "OrgAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgAccounts_Users_UserId",
                table: "OrgAccounts");

            migrationBuilder.DropIndex(
                name: "IX_OrgAccounts_UserId",
                table: "OrgAccounts");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "OrgAccounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrgAccounts");
        }
    }
}
