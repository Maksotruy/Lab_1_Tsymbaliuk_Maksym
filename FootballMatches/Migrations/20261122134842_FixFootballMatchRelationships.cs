
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.Migrations
{
    public partial class FixFootballMatchRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Видаляємо старі зовнішні ключі
            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Stadium_StadiumId",
                table: "FootballMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Team_AwayTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Team_HomeTeamReferenceId",
                table: "FootballMatches");

            // Видаляємо старі індекси
            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_AwayTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_HomeTeamReferenceId",
                table: "FootballMatches");

            // Переносимо ідентифікатори команд
            // зі старих стовпців у нові
            migrationBuilder.Sql(@"
                UPDATE FootballMatches
                SET HomeTeamId = HomeTeamReferenceId
                WHERE HomeTeamId IS NULL
                  AND HomeTeamReferenceId IS NOT NULL;
            ");

            migrationBuilder.Sql(@"
                UPDATE FootballMatches
                SET AwayTeamId = AwayTeamReferenceId
                WHERE AwayTeamId IS NULL
                  AND AwayTeamReferenceId IS NOT NULL;
            ");

            // Видаляємо зайві стовпці
            migrationBuilder.DropColumn(
                name: "AwayTeamReferenceId",
                table: "FootballMatches");

            migrationBuilder.DropColumn(
                name: "HomeTeamReferenceId",
                table: "FootballMatches");

            // Створюємо індекси для правильних ключів
            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_AwayTeamId",
                table: "FootballMatches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_HomeTeamId",
                table: "FootballMatches",
                column: "HomeTeamId");

            // Зв'язок зі стадіоном
            migrationBuilder.AddForeignKey(
                name: "FK_FootballMatches_Stadium_StadiumId",
                table: "FootballMatches",
                column: "StadiumId",
                principalTable: "Stadium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Зв'язок із гостьовою командою
            migrationBuilder.AddForeignKey(
                name: "FK_FootballMatches_Team_AwayTeamId",
                table: "FootballMatches",
                column: "AwayTeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Зв'язок із домашньою командою
            migrationBuilder.AddForeignKey(
                name: "FK_FootballMatches_Team_HomeTeamId",
                table: "FootballMatches",
                column: "HomeTeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Видаляємо нові зовнішні ключі
            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Stadium_StadiumId",
                table: "FootballMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Team_AwayTeamId",
                table: "FootballMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_FootballMatches_Team_HomeTeamId",
                table: "FootballMatches");

            // Видаляємо нові індекси
            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_AwayTeamId",
                table: "FootballMatches");

            migrationBuilder.DropIndex(
                name: "IX_FootballMatches_HomeTeamId",
                table: "FootballMatches");

            // Відновлюємо старі стовпці
            migrationBuilder.AddColumn<int>(
                name: "AwayTeamReferenceId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamReferenceId",
                table: "FootballMatches",
                type: "INTEGER",
                nullable: true);

            // Відновлюємо значення зв'язків
            migrationBuilder.Sql(@"
                UPDATE FootballMatches
                SET HomeTeamReferenceId = HomeTeamId,
                    AwayTeamReferenceId = AwayTeamId;
            ");

            // Відновлюємо старі індекси
            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_AwayTeamReferenceId",
                table: "FootballMatches",
                column: "AwayTeamReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballMatches_HomeTeamReferenceId",
                table: "FootballMatches",
                column: "HomeTeamReferenceId");

            // Відновлюємо старі зовнішні ключі
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
    }
}
