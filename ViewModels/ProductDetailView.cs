using MiniShop.Models;

namespace MiniShop.ViewModels;

public class ProductDetailView
{
    public Product Product { get; set; } = null!;
    public List<Category> Categories { get; set; } = null!;
}