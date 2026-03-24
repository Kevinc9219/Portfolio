using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CvAnalyzer.Services;

public class OpenAIService
{
    private readonly string _apiKey;
    private readonly string _model;
    private readonly HttpClient _httpClient;

    public OpenAIService(IConfiguration configuration)
    {
        _apiKey = configuration["OpenAI:ApiKey"] ?? "";
        _model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> AnalyzeCvAsync(string cvText)
    {
        var prompt = $"""
Je bent een ervaren HR-professional en cv-expert.
Analyseer het volgende cv en geef je feedback UITSLUITEND als geldig JSON, zonder markdown of toelichting:
{{
  "strengths": ["string", "string", "string"],
  "improvements": [{{"point": "string", "suggestion": "string"}}],
  "scores": {{"profile": 0, "experience": 0, "education": 0, "skills": 0}},
  "matchingJobs": ["string", "string", "string"],
  "summary": "string"
}}
CV: {cvText}
""";
        var requestBody = new
        {
            model = _model,
            messages = new[] { new { role = "user", content = prompt } }
        };

        for (int attempt = 0; attempt < 2; attempt++)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
            }
            catch (TaskCanceledException) when (attempt == 0) { await Task.Delay(1000); }
        }
        throw new Exception("API timeout na 2 pogingen.");
    }
}
