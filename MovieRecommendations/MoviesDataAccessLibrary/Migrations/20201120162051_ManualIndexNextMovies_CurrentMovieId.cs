using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesDataAccessLibrary.Migrations
{
    public partial class ManualIndexNextMovies_CurrentMovieId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "NextMovies_CurrentMovieId",
                table: "NextMovies",
                column: "CurrentMovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "NextMovies_CurrentMovieId",
                table: "NextMovies");
        }
    }
}
