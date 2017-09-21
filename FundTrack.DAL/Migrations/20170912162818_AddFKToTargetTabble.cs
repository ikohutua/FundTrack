using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundTrack.DAL.Migrations
{
    public partial class AddFKToTargetTabble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Targets_ParentTargetId",
                table: "Targets",
                column: "ParentTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Targets_ParentTargetId",
                table: "Targets",
                column: "ParentTargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Targets_ParentTargetId",
                table: "Targets");

            migrationBuilder.DropIndex(
                name: "IX_Targets_ParentTargetId",
                table: "Targets");
        }
    }
}
