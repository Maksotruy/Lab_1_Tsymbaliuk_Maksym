
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using FootballMatches.Data;
using FootballMatches.Models;
using FootballMatches.Services;
using Xunit;

namespace FootballMatches.Tests;

public class SummaryCalculatorTests : IDisposable
{
    private readonly SqliteConnection connection;
    private readonly FootballMatchesContext db;

    public SummaryCalculatorTests()
    {
        connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var options =
            new DbContextOptionsBuilder<FootballMatchesContext>()
                .UseSqlite(connection)
                .Options;

        db = new FootballMatchesContext(options);
        db.Database.EnsureCreated();
    }

    public void Dispose()
    {
        db.Dispose();
        connection.Dispose();
    }

    private async Task AddMatchAsync(
        string homeTeam,
        string awayTeam,
        int homeScore,
        int awayScore,
        DateOnly date)
    {
        var team = await db.Team
            .FirstOrDefaultAsync(t => t.Name == homeTeam);

        if (team == null)
        {
            team = new Team { Name = homeTeam };
            db.Team.Add(team);
            await db.SaveChangesAsync();
        }

        db.FootballMatches.Add(new FootballMatch
        {
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
            HomeScore = homeScore,
            AwayScore = awayScore,
            MatchDate = date,
            HomeTeamReference = team,
            Competition = "Test Competition"
        });

        await db.SaveChangesAsync();
    }

    [Fact]
    public async Task EmptyDatabaseReturnsNoTeamRows()
    {
        var result = await SummaryCalculator.ByTeamAsync(
            db.FootballMatches);

        Assert.Empty(result);
    }

    [Fact]
    public async Task MatchesOfOneTeamMergeIntoSingleRow()
    {
        await AddMatchAsync(
            "Arsenal", "Chelsea", 2, 1,
            new DateOnly(2026, 1, 10));

        await AddMatchAsync(
            "Arsenal", "Liverpool", 3, 0,
            new DateOnly(2026, 1, 15));

        var result = await SummaryCalculator.ByTeamAsync(
            db.FootballMatches);

        var row = Assert.Single(result);

        Assert.Equal("Arsenal", row.Name);
        Assert.Equal(2, row.Count);
        Assert.Equal(5, row.Total);
    }

    [Fact]
    public async Task TotalGoalsAreCalculatedCorrectly()
    {
        await AddMatchAsync(
            "Arsenal", "Chelsea", 2, 1,
            new DateOnly(2026, 2, 10));

        await AddMatchAsync(
            "Liverpool", "Arsenal", 4, 2,
            new DateOnly(2026, 2, 12));

        var result = await SummaryCalculator.TotalAsync(
            db.FootballMatches);

        Assert.Equal(2, result.Count);
        Assert.Equal(6, result.Total);
    }

    [Fact]
    public async Task DifferentMonthsProduceDifferentRows()
    {
        await AddMatchAsync(
            "Arsenal", "Chelsea", 2, 1,
            new DateOnly(2026, 3, 10));

        await AddMatchAsync(
            "Arsenal", "Liverpool", 3, 0,
            new DateOnly(2026, 4, 10));

        var result = await SummaryCalculator.ByMonthAsync(
            db.FootballMatches);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.Name == "03.2026");
        Assert.Contains(result, r => r.Name == "04.2026");
    }

    [Fact]
    public async Task SingleMatchProducesOneSummaryRow()
    {
        await AddMatchAsync(
            "Barcelona", "Real Madrid", 1, 0,
            new DateOnly(2026, 5, 10));

        var result = await SummaryCalculator.ByTeamAsync(
            db.FootballMatches);

        var row = Assert.Single(result);

        Assert.Equal("Barcelona", row.Name);
        Assert.Equal(1, row.Count);
        Assert.Equal(1, row.Total);
    }

    [Fact]
    public async Task EmptyDatabaseReturnsZeroTotal()
    {
        var result = await SummaryCalculator.TotalAsync(
            db.FootballMatches);

        Assert.Equal(0, result.Count);
        Assert.Equal(0, result.Total);
    }
}
