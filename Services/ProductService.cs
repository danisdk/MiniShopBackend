using AutoMapper;
using MiniShop.Models;

namespace MiniShop.Services;

public class ProductService : FrozenService<Product>
{
    
    protected IBaseService<Image> ImageService;
    public ProductService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper,
        ILogger<ProductService> logger, IBaseService<Image> imageService
    ) : base(context, httpContextAccessor, mapper, logger)
    {
        ImageService = imageService;
    }

    public override async Task AfterDeleteAsync(Product entity)
    {
        await base.AfterDeleteAsync(entity);
        Image? image = await Context.Images.FindAsync(entity.ImageId);
        if (image != null)
        {
            await ImageService.DeleteAsync(image);
        }
    }
}