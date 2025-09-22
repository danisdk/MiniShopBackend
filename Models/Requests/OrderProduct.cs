using MiniShop.Models.Responses;

namespace MiniShop.Models.Requests;

public class OrderProductRequest
{
    public int ProductId { get; set; }
    public ProductResponse Product { get; set; }
    public int Quantity { get; set; }
}

public class OrderProductAdd
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int? OrderProductId { get; set; }
}