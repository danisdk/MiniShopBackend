using AutoMapper;

namespace MiniShop.Models.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public UserResponse? AuthorCreated { get; set; }
    public UserResponse? AuthorUpdated { get; set; }
    public DateTime? DateTimeCreate { get; set; }
    public DateTime? DateTimeUpdate { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ImageResponse? Image { get; set; }
    public CategoryResponse? Category { get; set; }
    
}