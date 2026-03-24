using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AiTaakplanner.Data;
using AiTaakplanner.Models;
using AiTaakplanner.Services;

namespace AiTaakplanner.Controllers;

public class TasksController : Controller
{
    private readonly AppDbContext _db;
    private readonly TaskAnalyzerService _analyzer;

    public TasksController(AppDbContext db, TaskAnalyzerService analyzer) { _db = db; _analyzer = analyzer; }

    public async Task<IActionResult> Index(string? filter, string? category)
    {
        var query = _db.Tasks.AsQueryable();
        if (filter == "high") query = query.Where(t => (t.UserPriority ?? t.AiPriority) == Priority.Hoog);
        if (filter == "open") query = query.Where(t => !t.IsCompleted);
        if (filter == "done") query = query.Where(t => t.IsCompleted);
        if (!string.IsNullOrEmpty(category)) query = query.Where(t => (t.UserCategory ?? t.AiCategory) == category);
        var tasks = await query.OrderBy(t => t.UserPriority ?? t.AiPriority).ThenByDescending(t => t.CreatedAt).ToListAsync();

        // Statistieken
        var all = await _db.Tasks.ToListAsync();
        ViewBag.TotalTasks = all.Count;
        ViewBag.HighPriority = all.Count(t => t.EffectivePriority == Priority.Hoog && !t.IsCompleted);
        ViewBag.Completed = all.Count(t => t.IsCompleted);
        ViewBag.Categories = all.Select(t => t.EffectiveCategory).Distinct().OrderBy(c => c).ToList();
        return View(tasks);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskItem task, string? aiPriorityStr, string? aiCategory, int aiDuration, string? aiDeadline, string? aiReasoning)
    {
        if (!string.IsNullOrEmpty(aiPriorityStr))
        {
            task.AiPriority = aiPriorityStr switch { "hoog" => Priority.Hoog, "laag" => Priority.Laag, _ => Priority.Midden };
            task.AiCategory = aiCategory ?? "overig";
            task.AiDurationMinutes = aiDuration;
            task.AiReasoning = aiReasoning ?? "";
            if (!string.IsNullOrEmpty(aiDeadline) && DateTime.TryParse(aiDeadline, out var dl))
                task.Deadline = dl;
        }
        if (ModelState.IsValid) { _db.Tasks.Add(task); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        return View(task);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task != null) { task.IsCompleted = !task.IsCompleted; await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task != null) { _db.Tasks.Remove(task); await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}
