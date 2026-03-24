using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoorraadApp.Data;
using VoorraadApp.Models;
namespace VoorraadApp.Controllers;
public class SuppliersController : Controller
{
    private readonly AppDbContext _db;
    public SuppliersController(AppDbContext db) { _db = db; }
    public async Task<IActionResult> Index() => View(await _db.Suppliers.Include(s => s.Products).ToListAsync());
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Supplier supplier)
    {
        if (ModelState.IsValid) { _db.Suppliers.Add(supplier); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        return View(supplier);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var s = await _db.Suppliers.FindAsync(id); if (s == null) return NotFound(); return View(s);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Supplier supplier)
    {
        if (id != supplier.Id) return BadRequest();
        if (ModelState.IsValid) { _db.Suppliers.Update(supplier); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        return View(supplier);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var s = await _db.Suppliers.FindAsync(id); if (s == null) return NotFound(); return View(s);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var s = await _db.Suppliers.FindAsync(id); if (s != null) { _db.Suppliers.Remove(s); await _db.SaveChangesAsync(); } return RedirectToAction(nameof(Index));
    }
}
