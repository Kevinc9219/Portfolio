using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoorraadApp.Data;
using VoorraadApp.Models;
namespace VoorraadApp.Controllers;
public class ProductsController : Controller
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) { _db = db; }

    public async Task<IActionResult> Index(string? search, int? categoryId, int? supplierId)
    {
        var query = _db.Products.Include(p => p.Category).Include(p => p.Supplier).AsQueryable();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search) || p.SKU.Contains(search));
        if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId);
        if (supplierId.HasValue) query = query.Where(p => p.SupplierId == supplierId);
        ViewBag.Categories = await _db.Categories.ToListAsync();
        ViewBag.Suppliers = await _db.Suppliers.ToListAsync();
        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _db.Categories.ToListAsync();
        ViewBag.Suppliers = await _db.Suppliers.ToListAsync();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid) { _db.Products.Add(product); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        ViewBag.Categories = await _db.Categories.ToListAsync();
        ViewBag.Suppliers = await _db.Suppliers.ToListAsync();
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();
        ViewBag.Categories = await _db.Categories.ToListAsync();
        ViewBag.Suppliers = await _db.Suppliers.ToListAsync();
        return View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        if (ModelState.IsValid) { _db.Products.Update(product); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        ViewBag.Categories = await _db.Categories.ToListAsync();
        ViewBag.Suppliers = await _db.Suppliers.ToListAsync();
        return View(product);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _db.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null) { _db.Products.Remove(product); await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}
