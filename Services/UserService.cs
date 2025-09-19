using AutoMapper;
using MiniShop.Models;

namespace MiniShop.Services;

public class UserService : BaseService<User>
{
    public UserService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<UserService> logger
    ) : base(context, httpContextAccessor, mapper, logger){}
}

public class UserUnauthorizedException : Exception
{
    public UserUnauthorizedException(string message) : base(message) { }
}