using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.Migrations
{
    /// <inheritdoc />
    public partial class AddReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayTeamId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamReferenceId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamReferenceId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StadiumId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stadium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_AwayTeamReferenceId",
                table: "FootballMatches",
                column: "AwayTeamReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_HomeTeamReferenceId",
                table: "FootballMatches",
                column: "HomeTeamReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_StadiumId",
                table: "FootballMatches",
                column: "StadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_FootballMatches_Stadium_StadiumId",
                table: "FootballMatches",
                column: "StadiumId",
                principalTable: "Stadium",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FootballMatches_Team_AwayTeamReferenceId",
                table: "FootballMatches",
                column: "AwayTeamReferenceId",
                principalTable: "Team",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FootballMatches_Team_HomeTeamReferenceId",
                table: "FootballMatches",
                column: "HomeTeamReferenceId",
                principalTable: "Team",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Stadium_StadiumId",
                table: "FootballMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Team_AwayTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Team_HomeTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropTable(
                name: "Stadium");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_AwayTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_HomeTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_StadiumId",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "AwayTeamId",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "AwayTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "HomeTeamId",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "HomeTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "FootballMatches");
        }
    }
}
