using System.ComponentModel.DataAnnotations;

namespace MiniShop.Models;

public class Category : Frozen
{
    [MaxLength(100)]
    public string Name { get;set; }
    [MaxLength(100)]
    public string Slug { get;set; }
}