

using System.ComponentModel.DataAnnotations;

namespace MiniShop.Models;

public class RefreshToken : Base
{
    public int UserId { get; set; }
    public User User { get; set; }
    [MaxLength(100)]
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool Revoked { get; set; } = false;
}