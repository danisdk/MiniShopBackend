namespace MiniShop.Models.Responses;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class RegisterResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
}