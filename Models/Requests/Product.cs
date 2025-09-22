namespace MiniShop.Models.Requests;

public class ProductRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? ImageId { get; set; }
    public int? CategoryId { get; set; }
}

public class ProductOnlyIdRequest
{
    public int Id { get; set; }
}