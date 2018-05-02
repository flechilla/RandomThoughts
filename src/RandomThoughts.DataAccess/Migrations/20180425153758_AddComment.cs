using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomThoughts.DataAccess.Migrations
{
    public partial class AddComment : Migration
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

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Visibility = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Thoughts_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EntityId",
                table: "Comments",
                column: "EntityId");

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

            migrationBuilder.DropTable(
                name: "Comments");

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
