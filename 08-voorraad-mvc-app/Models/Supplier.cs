using System.ComponentModel.DataAnnotations;
namespace VoorraadApp.Models;
public class Supplier
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [EmailAddress] public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
