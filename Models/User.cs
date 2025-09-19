using System.ComponentModel.DataAnnotations;

namespace MiniShop.Models;

public class User : Base
{
    [MaxLength(255)]
    public string UserName { get; set; }
    [MaxLength(512)]
    public string Password { get; set; }
    [MaxLength(255)]
    public string? FirstName { get; set; }
    [MaxLength(255)]
    public string? LastName { get; set; }
    [MaxLength(255)]
    public string? Patronymic { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
}