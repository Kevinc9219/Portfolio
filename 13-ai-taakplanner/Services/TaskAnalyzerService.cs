using System.Text.Json;
using AiTaakplanner.Models;

namespace AiTaakplanner.Services;

public class TaskAnalyzerService
{
    private readonly OpenAIService _openAI;

    public TaskAnalyzerService(OpenAIService openAI) { _openAI = openAI; }

    public async Task<TaskAnalysisDto> AnalyzeAsync(string title)
    {
        var raw = await _openAI.AnalyzeTaskAsync(title);
        var clean = raw.Replace("```json", "").Replace("```", "").Trim();
        return JsonSerializer.Deserialize<TaskAnalysisDto>(clean) ?? new TaskAnalysisDto();
    }

    public Priority ParsePriority(string p) => p.ToLower() switch
    {
        "hoog" => Priority.Hoog,
        "laag" => Priority.Laag,
        _ => Priority.Midden
    };
}
