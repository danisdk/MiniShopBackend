using System.ComponentModel.DataAnnotations.Schema;

namespace MiniShop.Models;

public class Order : Frozen
{
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime? OrderDateTime { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public Decimal Total { get; set; } = decimal.Zero;
    public bool IsPaid { get; set; } = false;
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}