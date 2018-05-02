using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomThoughts.DataAccess.Migrations
{
    public partial class AddThoughtCommentsContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Thoughts_EntityId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment<Thought, int>");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_EntityId",
                table: "Comment<Thought, int>",
                newName: "IX_Comment<Thought, int>_EntityId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Comment<Thought, int>",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment<Thought, int>",
                table: "Comment<Thought, int>",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment<Thought, int>_Thoughts_EntityId",
                table: "Comment<Thought, int>",
                column: "EntityId",
                principalTable: "Thoughts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment<Thought, int>_Thoughts_EntityId",
                table: "Comment<Thought, int>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment<Thought, int>",
                table: "Comment<Thought, int>");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Comment<Thought, int>");

            migrationBuilder.RenameTable(
                name: "Comment<Thought, int>",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment<Thought, int>_EntityId",
                table: "Comments",
                newName: "IX_Comments_EntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Thoughts_EntityId",
                table: "Comments",
                column: "EntityId",
                principalTable: "Thoughts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
