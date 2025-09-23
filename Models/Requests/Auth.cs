namespace MiniShop.Models.Requests;

public class RegisterRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class RefreshPasswordRequest
{
    public string Password { get; set; }
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
}