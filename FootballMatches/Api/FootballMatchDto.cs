
namespace FootballMatches.Api;

public record FootballMatchDto(
    int Id,
    string HomeTeam,
    string AwayTeam,
    int HomeScore,
    int AwayScore,
    DateOnly MatchDate,
    string? Stadium,
    string Status,
    string Competition
);
