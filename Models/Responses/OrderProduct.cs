namespace MiniShop.Models.Responses;

public class OrderProductResponse
{
    public int Id { get; set; }
    public ProductResponse Product { get; set; }
    public int Quantity { get; set; } = 0;
}