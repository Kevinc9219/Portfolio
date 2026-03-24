using System.ComponentModel.DataAnnotations;

namespace AiTaakplanner.Models;

public enum Priority { Hoog = 1, Midden = 2, Laag = 3 }

public class TaskItem
{
    public int Id { get; set; }
    [Required] public string Title { get; set; } = "";
    public Priority AiPriority { get; set; } = Priority.Midden;
    public Priority? UserPriority { get; set; }
    public string AiCategory { get; set; } = "overig";
    public string? UserCategory { get; set; }
    public int AiDurationMinutes { get; set; }
    public DateTime? Deadline { get; set; }
    public string AiReasoning { get; set; } = "";
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Priority EffectivePriority => UserPriority ?? AiPriority;
    public string EffectiveCategory => UserCategory ?? AiCategory;
}
