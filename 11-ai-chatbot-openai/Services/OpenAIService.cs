using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using AiChatbot.Models;

namespace AiChatbot.Services;

public class OpenAIService
{
    private readonly string _apiKey;
    private readonly string _model;
    private readonly HttpClient _httpClient;

    public OpenAIService(IConfiguration configuration)
    {
        _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API key niet gevonden in appsettings.json");
        _model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<(string response, int tokensUsed)> SendMessageAsync(List<ChatMessage> history)
    {
        var messages = history.Select(m => new { role = m.Role, content = m.Content }).ToList();
        var requestBody = new { model = _model, messages };
        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        var content = root.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
        var tokens = root.GetProperty("usage").GetProperty("total_tokens").GetInt32();
        return (content, tokens);
    }
}
