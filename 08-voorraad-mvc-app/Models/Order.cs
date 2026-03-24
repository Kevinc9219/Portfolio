using System.ComponentModel.DataAnnotations;
namespace VoorraadApp.Models;
public enum OrderStatus { Pending, Shipped, Delivered }
public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    [Range(1, int.MaxValue)] public int Quantity { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
