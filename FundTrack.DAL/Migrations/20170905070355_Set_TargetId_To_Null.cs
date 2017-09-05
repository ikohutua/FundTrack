using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Set_TargetId_To_Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "TargetId",
                table: "OrgAccounts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "TargetId",
                table: "OrgAccounts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgAccounts_Targets_TargetId",
                table: "OrgAccounts",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
