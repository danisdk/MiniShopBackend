using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniShop.Models;
public class Product : Frozen
{
    [Required, MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public int? ImageId { get; set; }
    public Image? Image { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}