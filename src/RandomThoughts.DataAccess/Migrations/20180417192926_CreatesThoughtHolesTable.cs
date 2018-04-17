using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomThoughts.DataAccess.Migrations
{
    public partial class CreatesThoughtHolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThoughtHoleId",
                table: "Thoughts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThoughtHoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoughtHoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thoughts_ThoughtHoleId",
                table: "Thoughts",
                column: "ThoughtHoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thoughts_ThoughtHoles_ThoughtHoleId",
                table: "Thoughts",
                column: "ThoughtHoleId",
                principalTable: "ThoughtHoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thoughts_ThoughtHoles_ThoughtHoleId",
                table: "Thoughts");

            migrationBuilder.DropTable(
                name: "ThoughtHoles");

            migrationBuilder.DropIndex(
                name: "IX_Thoughts_ThoughtHoleId",
                table: "Thoughts");

            migrationBuilder.DropColumn(
                name: "ThoughtHoleId",
                table: "Thoughts");
        }
    }
}
