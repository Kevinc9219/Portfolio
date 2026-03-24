using System.Text.Json.Serialization;

namespace AiTaakplanner.Models;

public class TaskAnalysisDto
{
    [JsonPropertyName("priority")] public string Priority { get; set; } = "midden";
    [JsonPropertyName("category")] public string Category { get; set; } = "overig";
    [JsonPropertyName("estimatedMinutes")] public int EstimatedMinutes { get; set; } = 30;
    [JsonPropertyName("deadline")] public string? Deadline { get; set; }
    [JsonPropertyName("reasoning")] public string Reasoning { get; set; } = "";
}
