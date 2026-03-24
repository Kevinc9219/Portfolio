using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoorraadApp.Models;

public class Product
{
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; } = "";
    [Required, StringLength(50)]
    public string SKU { get; set; } = "";
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
