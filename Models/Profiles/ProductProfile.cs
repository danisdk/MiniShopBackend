using AutoMapper;
using MiniShop.Models.Requests;
using MiniShop.Models.Responses;

namespace MiniShop.Models.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, Product>().ForMember(
            dest => dest.Id, opt => opt.Ignore()
            );
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductRequest, Product>();
    }
}