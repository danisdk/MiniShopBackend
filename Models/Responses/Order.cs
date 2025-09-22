namespace MiniShop.Models.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public UserResponse? AuthorCreated { get; set; }
    public UserResponse? AuthorUpdated { get; set; }
    public DateTime? DateTimeCreate { get; set; }
    public DateTime? DateTimeUpdate { get; set; }
    public UserResponse User { get; set; }
    public DateTime? OrderDateTime { get; set; }
    public Decimal Total { get; set; }
    public bool IsPaid { get; set; }
    public List<OrderProductResponse> OrderProducts { get; set; }
}