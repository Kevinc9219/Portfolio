using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StageTracker.Data;
using StageTracker.Models;
namespace StageTracker.Controllers;
public class CompanyController : Controller
{
    private readonly AppDbContext _db;
    public CompanyController(AppDbContext db) { _db = db; }
    public async Task<IActionResult> Index() => View(await _db.Companies.FirstOrDefaultAsync());
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Company company)
    {
        if (ModelState.IsValid) { _db.Companies.Add(company); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        return View(company);
    }
}
