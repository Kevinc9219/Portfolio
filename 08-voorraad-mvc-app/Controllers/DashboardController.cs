using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoorraadApp.Data;
namespace VoorraadApp.Controllers;
public class DashboardController : Controller
{
    private readonly AppDbContext _db;
    public DashboardController(AppDbContext db) { _db = db; }
    public async Task<IActionResult> Index()
    {
        ViewBag.TotalProducts = await _db.Products.CountAsync();
        ViewBag.LowStock = await _db.Products.CountAsync(p => p.StockQuantity < 10);
        ViewBag.OpenOrders = await _db.Orders.CountAsync(o => o.Status == Models.OrderStatus.Pending);
        ViewBag.TotalSuppliers = await _db.Suppliers.CountAsync();
        ViewBag.LowStockProducts = await _db.Products.Include(p => p.Category).Where(p => p.StockQuantity < 10).ToListAsync();
        return View();
    }
}
