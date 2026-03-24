using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoorraadApp.Data;
using VoorraadApp.Models;
namespace VoorraadApp.Controllers;
public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) { _db = db; }
    public async Task<IActionResult> Index() => View(await _db.Orders.Include(o => o.Product).OrderByDescending(o => o.OrderDate).ToListAsync());
    public async Task<IActionResult> Create()
    {
        ViewBag.Products = await _db.Products.ToListAsync(); return View();
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        if (ModelState.IsValid) { _db.Orders.Add(order); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        ViewBag.Products = await _db.Products.ToListAsync(); return View(order);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order != null) { order.Status = status; await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}
