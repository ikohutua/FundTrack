using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Added_Column_TargetId_Into_OrgAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "OrgAccounts",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrgAccounts_TargetId",
                table: "OrgAccounts",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts");

            migrationBuilder.DropIndex(
                name: "IX_OrgAccounts_TargetId",
                table: "OrgAccounts");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "OrgAccounts");
        }
    }
}
