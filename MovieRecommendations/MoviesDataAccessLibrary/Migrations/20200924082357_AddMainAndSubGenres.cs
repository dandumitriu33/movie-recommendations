using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesDataAccessLibrary.Migrations
{
    public partial class AddMainAndSubGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainGenre",
                table: "Movies",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubGenre1",
                table: "Movies",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubGenre2",
                table: "Movies",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainGenre",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "SubGenre1",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "SubGenre2",
                table: "Movies");
        }
    }
}
