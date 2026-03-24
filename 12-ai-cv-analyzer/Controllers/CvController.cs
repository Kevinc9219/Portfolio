using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using CvAnalyzer.Data;
using CvAnalyzer.Models;
using CvAnalyzer.Services;

namespace CvAnalyzer.Controllers;

public class CvController : Controller
{
    private readonly AppDbContext _db;
    private readonly OpenAIService _openAI;
    private readonly PdfParserService _pdfParser;

    public CvController(AppDbContext db, OpenAIService openAI, PdfParserService pdfParser)
    { _db = db; _openAI = openAI; _pdfParser = pdfParser; }

    public IActionResult Index() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Analyze(string? cvText, IFormFile? pdfFile)
    {
        // PDF verwerking
        if (pdfFile != null && pdfFile.Length > 0)
        {
            if (pdfFile.Length > 5 * 1024 * 1024)
            { ModelState.AddModelError("", "PDF mag maximaal 5MB zijn."); return View("Index"); }
            using var stream = pdfFile.OpenReadStream();
            cvText = _pdfParser.ExtractText(stream);
        }

        if (string.IsNullOrWhiteSpace(cvText) || cvText.Length < 50)
        { ModelState.AddModelError("", "Voer een CV in van minimaal 50 tekens."); return View("Index"); }

        try
        {
            var rawJson = await _openAI.AnalyzeCvAsync(cvText);
            var cleanJson = rawJson.Replace("```json", "").Replace("```", "").Trim();
            var result = JsonSerializer.Deserialize<AnalysisResult>(cleanJson)
                ?? throw new Exception("Ongeldige JSON ontvangen van OpenAI.");
            var analysis = new CvAnalysis { CvText = cvText, ResultJson = cleanJson, Summary = result.Summary };
            _db.CvAnalyses.Add(analysis);
            await _db.SaveChangesAsync();
            ViewBag.AnalysisId = analysis.Id;
            return View("Result", result);
        }
        catch (JsonException ex)
        {
            ModelState.AddModelError("", $"Fout bij verwerken van AI-respons: {ex.Message}");
            return View("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"API fout: {ex.Message}");
            return View("Index");
        }
    }

    public async Task<IActionResult> History() =>
        View(await _db.CvAnalyses.OrderByDescending(a => a.AnalyzedAt).ToListAsync());
}
