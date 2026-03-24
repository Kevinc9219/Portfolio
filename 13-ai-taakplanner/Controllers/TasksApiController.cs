using Microsoft.AspNetCore.Mvc;
using AiTaakplanner.Services;

namespace AiTaakplanner.Controllers;

[ApiController, Route("api/tasks")]
public class TasksApiController : ControllerBase
{
    private readonly TaskAnalyzerService _analyzer;

    public TasksApiController(TaskAnalyzerService analyzer) { _analyzer = analyzer; }

    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze([FromBody] AnalyzeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title)) return BadRequest("Titel is verplicht.");
        try
        {
            var result = await _analyzer.AnalyzeAsync(request.Title);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    public record AnalyzeRequest(string Title);
}
