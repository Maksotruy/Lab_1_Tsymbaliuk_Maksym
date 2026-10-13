using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.Migrations
{
    /// <inheritdoc />
    public partial class AddCompetitionToFootballMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Competition",
                table: "FootballMatches",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competition",
                table: "FootballMatches");
        }
    }
}
