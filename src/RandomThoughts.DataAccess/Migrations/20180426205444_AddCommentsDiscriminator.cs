using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomThoughts.DataAccess.Migrations
{
    public partial class AddCommentsDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment<Thought, int>");

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
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    ParentDiscriminator = table.Column<int>(nullable: false),
                    ThoughtId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Thoughts_ThoughtId",
                        column: x => x.ThoughtId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ThoughtId",
                table: "Comments",
                column: "ThoughtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.CreateTable(
                name: "Comment<Thought, int>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment<Thought, int>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment<Thought, int>_Thoughts_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment<Thought, int>_EntityId",
                table: "Comment<Thought, int>",
                column: "EntityId");
        }
    }
}
