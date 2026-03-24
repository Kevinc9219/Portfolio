using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StageTracker.Data;
namespace StageTracker.Controllers;
public class DashboardController : Controller
{
    private readonly AppDbContext _db;
    public DashboardController(AppDbContext db) { _db = db; }
    public async Task<IActionResult> Index()
    {
        var days = await _db.StageDays.OrderBy(d => d.Date).ToListAsync();
        int completed = days.Count;
        double progress = (completed / 60.0) * 100;
        double avgHours = days.Any() ? days.Average(d => d.HoursWorked) : 0;
        double avgMood = days.Any() ? days.Average(d => d.MoodScore) : 0;
        DateTime? startDate = days.Any() ? days.First().Date : null;
        DateTime? expectedEnd = startDate.HasValue ? startDate.Value.AddDays(60 * 1.4) : null;
        ViewBag.CompletedDays = completed;
        ViewBag.RemainingDays = 60 - completed;
        ViewBag.Progress = Math.Round(progress, 1);
        ViewBag.AvgHours = Math.Round(avgHours, 1);
        ViewBag.AvgMood = Math.Round(avgMood, 1);
        ViewBag.StartDate = startDate?.ToString("dd/MM/yyyy") ?? "–";
        ViewBag.ExpectedEnd = expectedEnd?.ToString("dd/MM/yyyy") ?? "–";
        ViewBag.RecentDays = days.OrderByDescending(d => d.Date).Take(5).ToList();
        return View();
    }
}
