using AutoMapper;
using MiniShop.Models;

namespace MiniShop.Services;

public class ProductService : FrozenService<Product>
{
    public ProductService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<ProductService> logger
    ) : base(context, httpContextAccessor, mapper, logger){}
}