using System.ComponentModel.DataAnnotations;
namespace StageTracker.Models;
public class Company
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    [Required] public string ContactPerson { get; set; } = "";
    [EmailAddress] public string ContactEmail { get; set; } = "";
    public string ContactPhone { get; set; } = "";
    public ICollection<StageDay> StageDays { get; set; } = new List<StageDay>();
}
