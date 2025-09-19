namespace MiniShop.Models.Responses;

public class CategoryResponse
{
    public int Id { get; set; }
    public UserResponse? AuthorCreated { get; set; }
    public UserResponse? AuthorUpdated { get; set; }
    public DateTime? DateTimeCreate { get; set; }
    public DateTime? DateTimeUpdate { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
}