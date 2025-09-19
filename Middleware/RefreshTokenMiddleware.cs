namespace MiniShop.Middleware;

public class RefreshTokenMiddleware
{

    private readonly RequestDelegate _next;
    
    public RefreshTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        
        bool hasRefresh = context.Request.Cookies.ContainsKey("RefreshToken");
        if (
            hasRefresh && 
            (!context.User.Identity?.IsAuthenticated ?? false) && 
            !context.Request.Path.StartsWithSegments("/auth/refresh")
            )
        {
            string returnUrl = Uri.EscapeDataString(context.Request.Path + context.Request.QueryString);
            context.Response.Redirect($"/auth/refresh?returnUrl={returnUrl}");
            return;
        }
        await _next(context);
    }
}