
using System.ComponentModel.DataAnnotations;

namespace FootballMatches.Models;

public class FootballMatch
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Вкажіть назву домашньої команди")]
    [StringLength(100, ErrorMessage = "Назва домашньої команди не може перевищувати 100 символів")]
    public string HomeTeam { get; set; } = "";

    [Required(ErrorMessage = "Вкажіть назву гостьової команди")]
    [StringLength(100, ErrorMessage = "Назва гостьової команди не може перевищувати 100 символів")]
    public string AwayTeam { get; set; } = "";

    [Range(0, 100, ErrorMessage = "Кількість голів домашньої команди має бути від 0 до 100")]
    public int HomeScore { get; set; }

    [Range(0, 100, ErrorMessage = "Кількість голів гостьової команди має бути від 0 до 100")]
    public int AwayScore { get; set; }

    [Required(ErrorMessage = "Вкажіть дату проведення матчу")]
    public DateOnly MatchDate { get; set; }

    [StringLength(150, ErrorMessage = "Назва стадіону не може перевищувати 150 символів")]
    public string? Stadium { get; set; }

    public MatchStatus Status { get; set; }
}
