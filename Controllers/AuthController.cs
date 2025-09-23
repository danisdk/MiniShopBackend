using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniShop.Models;
using MiniShop.Models.Requests;
using MiniShop.Models.Responses;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private ApplicationContext _context;
    private ILogger<AuthController> _logger;
    
    public AuthController(ApplicationContext context, ILogger<AuthController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Models.Requests.RegisterRequest request)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
        if (user is null)
        {
            return Unauthorized();
        }

        PasswordHasher<User> hasher = new PasswordHasher<User>();
        PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.Password, request.Password);
        if (result == PasswordVerificationResult.Success)
        {
            string encodedJwt = GenerateJwt(user);
            string refreshToken = GenerateRefreshToken();
            IEnumerable<RefreshToken> oldRefreshTokens = 
                await _context.RefreshTokens.Where(token => token.UserId == user.Id && token.Revoked == false).ToListAsync();
            foreach (RefreshToken oldRefreshToken in oldRefreshTokens)
            {
                oldRefreshToken.Revoked = true;
            }
            RefreshToken entityRefreshToken = new RefreshToken()
            {
                UserId = user.Id,
                Token = HashToken(refreshToken),
                Expires = DateTime.UtcNow.Add(AuthOptions.RefreshTokenLifetime)
            };
            await _context.RefreshTokens.AddAsync(entityRefreshToken);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Пользователь: {user.Id} авторизовался");
            TokenResponse tokenResponse = new TokenResponse
            {
                AccessToken = encodedJwt, 
                RefreshToken = refreshToken
            };
            return Ok(tokenResponse);
        }
        _logger.LogInformation($"Попытка авторизации пользователя: {user.Id}");
        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Models.Requests.RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.UserName == request.UserName))
        {
            return BadRequest("Пользователь с таким именем уже существует");
        }

        User user = new User
        {
            UserName = request.UserName
        };
        
        PasswordHasher<User> hasher = new PasswordHasher<User>();
        user.Password = hasher.HashPassword(user, request.Password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Пользователь: {user.Id} зарегестрировался");
        RegisterResponse registerResponse = new RegisterResponse
        {
            UserId = user.Id,
            UserName = user.UserName
        };
        return Ok(registerResponse);
    }
    
    [Authorize]
    [HttpPost("refresh-password")]
    public async Task<IActionResult> RefreshPassword([FromBody] RefreshPasswordRequest request)
    {
        User? currentUser = HttpContext.Items["CurrentUser"] as User;
        if (currentUser == null)
        {
            return Unauthorized();
        }
        
        PasswordHasher<User> hasher = new PasswordHasher<User>();
        currentUser.Password = hasher.HashPassword(currentUser, request.Password);
        _context.Users.Update(currentUser);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Пользователь: {currentUser.Id} обновил пароль");
        RegisterResponse registerResponse = new RegisterResponse
        {
            UserId = currentUser.Id,
            UserName = currentUser.UserName
        };
        return Ok(registerResponse);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshToken)
    {
        string hashRefreshToken = HashToken(refreshToken.RefreshToken);
        RefreshToken? entityRefreshToken = await _context.RefreshTokens.Include(
            token => token.User
            ).FirstOrDefaultAsync(token => token.Token == hashRefreshToken);
        if (entityRefreshToken is null || entityRefreshToken.Expires < DateTime.UtcNow)
        {
            return Unauthorized();
        }

        string accessToken = GenerateJwt(entityRefreshToken.User);
        _logger.LogInformation($"new access token: {accessToken}");
        string newRefreshToken = GenerateRefreshToken();
        RefreshToken newEntityRefreshToken = new RefreshToken()
        {
            UserId = entityRefreshToken.UserId,
            Token = HashToken(newRefreshToken),
            Expires = entityRefreshToken.Expires
        };
        await _context.RefreshTokens.AddAsync(newEntityRefreshToken);
        entityRefreshToken.Revoked = true;
        await _context.SaveChangesAsync();
        TokenResponse tokenResponse = new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
        return Ok(tokenResponse);
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        User? currentUser = HttpContext.Items["CurrentUser"] as User;
        if (currentUser is null)
        {
            return Ok();
        }
        
        IEnumerable<RefreshToken> refreshTokens = 
            await _context.RefreshTokens.Where(
                token => token.UserId == currentUser.Id && token.Revoked == false
                ).ToListAsync();
        foreach (RefreshToken refreshToken in refreshTokens)
        {
            refreshToken.Revoked = true;
        }
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Пользователь: {currentUser.Id} вышел из системы");
        return Ok();
    }

    public static string GenerateJwt(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        SymmetricSecurityKey key = AuthOptions.GetSymmetricSecurityKey();
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(AuthOptions.AccessTokenLifetime),
            signingCredentials: signingCredentials
        );
        string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }
    
    public static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }
    
    public static string HashToken(string token)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }
    
}