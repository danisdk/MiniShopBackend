using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MiniShop;
using MiniShop.Middleware;
using MiniShop.Models;
using MiniShop.Services;

var builder = WebApplication.CreateBuilder(args);

AuthOptions.Init(builder.Configuration);

string connection = builder.Configuration.GetConnectionString("LocalHostConnection")!;

builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.Services.AddControllersWithViews().AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidAudience = AuthOptions.Audience,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    }
    );

builder.Services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));

Type[] modelServiceInterfaces = new [] { typeof(IBaseService<>) };

Assembly assembly = typeof(Program).Assembly;
IEnumerable<Type> serviceTypes = assembly.GetTypes().Where(type =>
    {
        Type? baseType = type.BaseType;
        while (!type.IsAbstract && baseType != null)
        {
            if (baseType!.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(BaseService<>))
            {
                return true;
            }
            baseType = baseType.BaseType;
        }
        return false;
    }
    );

foreach (Type serviceType in serviceTypes)
{
    IEnumerable<Type> typeInterfaces = serviceType.GetInterfaces().Where(@interface =>
        @interface.IsGenericType &&
        modelServiceInterfaces.Contains(@interface.GetGenericTypeDefinition())
        );

    builder.Services.AddScoped(serviceType);
    foreach (Type typeInterface in typeInterfaces)
    {
        builder.Services.AddScoped(typeInterface, sp => sp.GetRequiredService(serviceType));
    }
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseExceptionHandler("/error");

app.UseMiddleware<RequestTimingMiddleware>();

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Constants.UploadPath),
    RequestPath = Constants.UploadUrl
});

app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddleware>();

app.Run();

public static class AuthOptions
{
    public static string Issuer = null!; // издатель токена
    public static string Audience = null!; // потребитель токена
    public static string Key = null!;   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    public static TimeSpan AccessTokenLifetime;
    public static TimeSpan RefreshTokenLifetime;
    public static void Init(IConfiguration config)
    {
        IConfigurationSection section = config.GetSection("Auth");
        Issuer = section.GetValue<string>("Issuer") ?? "MyAuthServer";
        Audience = section.GetValue<string>("Audience") ?? "MyAuthClient";
        Key = section.GetValue<string>("Key") ?? "secretsecretsecretkey!123";
        AccessTokenLifetime = TimeSpan.FromMinutes(section.GetValue<int>("AccessTokenLifetime"));
        RefreshTokenLifetime = TimeSpan.FromDays(section.GetValue<int>("RefreshTokenLifetime"));
    }
}

public static class Constants
{
    public static readonly string UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
    public static readonly string UploadUrl = "/uploads";
    public static readonly long MaxFileSize = 10 * 1024 * 1024;
}