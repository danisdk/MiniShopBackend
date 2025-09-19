using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MiniShop.Middleware;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationContext db)
    {
        var userClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim != null && int.TryParse(userClaim.Value, out int userId))
        {
            var user = await db.Users.FindAsync(userId);
            context.Items["CurrentUser"] = user;
        }

        await _next(context);
    }
}