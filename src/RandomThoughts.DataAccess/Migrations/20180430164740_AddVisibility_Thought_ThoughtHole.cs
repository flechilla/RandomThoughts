using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomThoughts.DataAccess.Migrations
{
    public partial class AddVisibility_Thought_ThoughtHole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thoughts_ThoughtHoles_ThoughtHoleId",
                table: "Thoughts");

            migrationBuilder.AlterColumn<int>(
                name: "ThoughtHoleId",
                table: "Thoughts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "Thoughts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "ThoughtHoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Thoughts_ThoughtHoles_ThoughtHoleId",
                table: "Thoughts",
                column: "ThoughtHoleId",
                principalTable: "ThoughtHoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thoughts_ThoughtHoles_ThoughtHoleId",
                table: "Thoughts");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Thoughts");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "ThoughtHoles");

            migrationBuilder.AlterColumn<int>(
                name: "ThoughtHoleId",
                table: "Thoughts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Thoughts_ThoughtHoles_ThoughtHoleId",
                table: "Thoughts",
                column: "ThoughtHoleId",
                principalTable: "ThoughtHoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
