using System.ComponentModel.DataAnnotations;
namespace StageTracker.Models;
public class Competence
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public ICollection<DayCompetence> DayCompetences { get; set; } = new List<DayCompetence>();
}
