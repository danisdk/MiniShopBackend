using AutoMapper;
using MiniShop.Models;

namespace MiniShop.Services;

public class CategoryService : FrozenService<Category>
{
    public CategoryService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<CategoryService> logger
    ) : base(context, httpContextAccessor, mapper, logger){}
}