using MiniShop.Models;

namespace MiniShop.ViewModels;

public class CartListView
{
    public Product Product { get; set; } = null!;
    public decimal SumTotal { get; set; }
    public int Quantity { get; set; }
}