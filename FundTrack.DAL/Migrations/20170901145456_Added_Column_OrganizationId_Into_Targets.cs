using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class Added_Column_OrganizationId_Into_Targets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Targets",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Targets_OrganizationId",
                table: "Targets",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Organizations_OrganizationId",
                table: "Targets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
            //onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Organizations_OrganizationId",
                table: "Targets");

            migrationBuilder.DropIndex(
                name: "IX_Targets_OrganizationId",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Targets");
        }
    }
}
