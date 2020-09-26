using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesDataAccessLibrary.Migrations
{
    public partial class AddCommunityLikesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunityLikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 120, nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityLikes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityLikes");
        }
    }
}
