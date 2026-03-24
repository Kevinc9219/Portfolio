using System.ComponentModel.DataAnnotations;

namespace CvAnalyzer.Models;

public class CvAnalysis
{
    public int Id { get; set; }
    public DateTime AnalyzedAt { get; set; } = DateTime.Now;
    [Required] public string CvText { get; set; } = "";
    public string ResultJson { get; set; } = "";
    public string Summary { get; set; } = "";
}
