
using System.ComponentModel.DataAnnotations;

namespace FootballMatches.Models;

public class Team
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Вкажіть назву команди")]
    [StringLength(100, ErrorMessage = "Назва команди не може перевищувати 100 символів")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Вкажіть країну команди")]
    [StringLength(100, ErrorMessage = "Назва країни не може перевищувати 100 символів")]
    public string Country { get; set; } = "";
}
