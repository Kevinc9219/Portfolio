using Microsoft.Extensions.Configuration;
using AiChatbot.Models;
using AiChatbot.Services;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

var openAIService = new OpenAIService(config);
var history = new List<ChatMessage>();
string systemPrompt = "Je bent een behulpzame IT-assistent voor studenten Graduaat Programmeren.";

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   AI Chatbot – Powered by OpenAI    ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine("Commando's: /reset  /exit  /persona [tekst]");
Console.WriteLine();

history.Add(new ChatMessage("system", systemPrompt));
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Bot: Hallo! Ik ben je AI-assistent. Persona: \"{systemPrompt}\"");
Console.ResetColor();

while (true)
{
    Console.Write("\nJij: ");
    var input = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(input)) continue;

    if (input == "/exit") { Console.WriteLine("Tot ziens!"); break; }
    if (input == "/reset")
    {
        history.Clear();
        history.Add(new ChatMessage("system", systemPrompt));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Gesprek gewist.");
        Console.ResetColor();
        continue;
    }
    if (input.StartsWith("/persona "))
    {
        systemPrompt = input[9..];
        history[0] = new ChatMessage("system", systemPrompt);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Persona ingesteld: {systemPrompt}");
        Console.ResetColor();
        continue;
    }

    history.Add(new ChatMessage("user", input));

    try
    {
        Console.Write("Bot: ");
        var (response, tokens) = await openAIService.SendMessageAsync(history);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(response);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[Tokens gebruikt: {tokens}]");
        Console.ResetColor();
        history.Add(new ChatMessage("assistant", response));

        // Trim history als te lang (bewaar altijd system message)
        if (history.Count > 21)
        {
            history.RemoveAt(1);
            history.RemoveAt(1);
        }
    }
    catch (HttpRequestException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Fout: geen internetverbinding.");
        Console.ResetColor();
        history.RemoveAt(history.Count - 1);
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"API fout: controleer je sleutel. ({ex.Message})");
        Console.ResetColor();
        history.RemoveAt(history.Count - 1);
    }
}
