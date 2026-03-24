using System.ComponentModel.DataAnnotations;
namespace StageTracker.Models;
public class StageDay
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    [Range(1, 24)] public double HoursWorked { get; set; }
    public string Activities { get; set; } = "";
    public string LearningPoints { get; set; } = "";
    [Range(1, 5)] public int MoodScore { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    public ICollection<DayCompetence> DayCompetences { get; set; } = new List<DayCompetence>();
}
