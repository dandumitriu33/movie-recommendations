using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesDataAccessLibrary.Migrations
{
    public partial class AddCreatorEmailToParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Parties",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Parties");
        }
    }
}
