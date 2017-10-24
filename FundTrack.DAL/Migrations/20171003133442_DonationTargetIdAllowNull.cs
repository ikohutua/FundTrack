using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class DonationTargetIdAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Target",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_TargetId",
                table:"Donations");

            migrationBuilder.AlterColumn<int>(
                name: "TargetId",
                table: "Donations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Donations_TargetId",
                table: "Donations",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Target",
                table: "Donations",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Target",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_TargetId",
                table: "Donations");

            migrationBuilder.AlterColumn<int>(
                name: "TargetId",
                table: "Donations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_TargetId",
                table: "Donations",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Target",
                table: "Donations",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
