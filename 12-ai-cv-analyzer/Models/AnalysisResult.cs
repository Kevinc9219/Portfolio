using System.Text.Json.Serialization;

namespace CvAnalyzer.Models;

public class AnalysisResult
{
    [JsonPropertyName("strengths")] public List<string> Strengths { get; set; } = new();
    [JsonPropertyName("improvements")] public List<Improvement> Improvements { get; set; } = new();
    [JsonPropertyName("scores")] public ScoreSection Scores { get; set; } = new();
    [JsonPropertyName("matchingJobs")] public List<string> MatchingJobs { get; set; } = new();
    [JsonPropertyName("summary")] public string Summary { get; set; } = "";
}

public class Improvement
{
    [JsonPropertyName("point")] public string Point { get; set; } = "";
    [JsonPropertyName("suggestion")] public string Suggestion { get; set; } = "";
}

public class ScoreSection
{
    [JsonPropertyName("profile")] public int Profile { get; set; }
    [JsonPropertyName("experience")] public int Experience { get; set; }
    [JsonPropertyName("education")] public int Education { get; set; }
    [JsonPropertyName("skills")] public int Skills { get; set; }
}
