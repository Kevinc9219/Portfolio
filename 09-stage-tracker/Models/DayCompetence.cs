namespace StageTracker.Models;
public class DayCompetence
{
    public int StageDayId { get; set; }
    public StageDay? StageDay { get; set; }
    public int CompetenceId { get; set; }
    public Competence? Competence { get; set; }
}
