namespace MiniShop.Models;

public abstract class Frozen : Base
{
    public int? AuthorCreatedId { get; set; }
    public User? AuthorCreated { get; set; }
    public int? AuthorUpdatedId { get; set; }
    public User? AuthorUpdated { get; set; }
    public DateTime? DateTimeCreate { get; set; }
    public DateTime? DateTimeUpdate { get; set; }
}