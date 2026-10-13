
using System.ComponentModel.DataAnnotations;

namespace FootballMatches.Models;

public class Stadium
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Вкажіть назву стадіону")]
    [StringLength(150, ErrorMessage = "Назва стадіону не може перевищувати 150 символів")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Вкажіть місто")]
    [StringLength(100, ErrorMessage = "Назва міста не може перевищувати 100 символів")]
    public string City { get; set; } = "";
}
