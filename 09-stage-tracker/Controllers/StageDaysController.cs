using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StageTracker.Data;
using StageTracker.Models;
namespace StageTracker.Controllers;
public class StageDaysController : Controller
{
    private readonly AppDbContext _db;
    public StageDaysController(AppDbContext db) { _db = db; }
    public async Task<IActionResult> Index() =>
        View(await _db.StageDays.Include(d => d.DayCompetences).ThenInclude(dc => dc.Competence).OrderByDescending(d => d.Date).ToListAsync());
    public async Task<IActionResult> Create()
    {
        ViewBag.Competences = await _db.Competences.ToListAsync();
        ViewBag.Companies = await _db.Companies.ToListAsync();
        return View();
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StageDay day, int[] selectedCompetences)
    {
        if (ModelState.IsValid)
        {
            _db.StageDays.Add(day);
            await _db.SaveChangesAsync();
            foreach (var cId in selectedCompetences)
                _db.DayCompetences.Add(new DayCompetence { StageDayId = day.Id, CompetenceId = cId });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Competences = await _db.Competences.ToListAsync();
        ViewBag.Companies = await _db.Companies.ToListAsync();
        return View(day);
    }
    public async Task<IActionResult> Details(int id)
    {
        var day = await _db.StageDays.Include(d => d.DayCompetences).ThenInclude(dc => dc.Competence).FirstOrDefaultAsync(d => d.Id == id);
        if (day == null) return NotFound();
        return View(day);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var day = await _db.StageDays.FindAsync(id);
        if (day != null) { _db.StageDays.Remove(day); await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}
