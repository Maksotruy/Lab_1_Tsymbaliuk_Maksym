using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.Migrations
{
    /// <inheritdoc />
    public partial class UniqueFootballMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_HomeTeam_AwayTeam_MatchDate",
                table: "FootballMatches",
                columns: new[] { "HomeTeam", "AwayTeam", "MatchDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_HomeTeam_AwayTeam_MatchDate",
                table: "FootballMatches");
        }
    }
}
