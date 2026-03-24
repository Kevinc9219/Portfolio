using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace AiTaakplanner.Services;

public class OpenAIService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public OpenAIService(IConfiguration configuration)
    {
        _apiKey = configuration["OpenAI:ApiKey"] ?? "";
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> AnalyzeTaskAsync(string title)
    {
        var prompt = $"""
Analyseer de volgende taak en geef UITSLUITEND geldig JSON terug, zonder markdown:
{{
  "priority": "hoog" | "midden" | "laag",
  "category": "werk" | "persoonlijk" | "studie" | "administratie" | "gezondheid" | "overig",
  "estimatedMinutes": getal,
  "deadline": "YYYY-MM-DD" of null,
  "reasoning": "één zin uitleg"
}}
Taak: "{title}"
Vandaag: {DateTime.Today:yyyy-MM-dd}
""";
        var requestBody = new { model = "gpt-4o-mini", messages = new[] { new { role = "user", content = prompt } } };
        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "{}";
    }
}
