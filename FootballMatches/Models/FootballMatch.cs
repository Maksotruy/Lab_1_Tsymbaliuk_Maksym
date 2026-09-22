namespace FootballMatches.Models;

public class FootballMatch
{
    public int Id { get; set; }

    public string HomeTeam { get; set; } = "";

    public string AwayTeam { get; set; } = "";

    public int HomeScore { get; set; }

    public int AwayScore { get; set; }

    public DateOnly MatchDate { get; set; }

    public string? Stadium { get; set; }

    public MatchStatus Status { get; set; }
}