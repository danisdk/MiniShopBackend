using AutoMapper;
using MiniShop.Models.Responses;

namespace MiniShop.Models.Profiles;

public class OrderProductProfile : Profile
{
    public OrderProductProfile()
    {
        CreateMap<OrderProduct, OrderProduct>().ForMember(
            dest => dest.Id, opt => opt.Ignore()
        ).ForMember(
            dest => dest.Order, opt => opt.Ignore()
            ).ForMember(
            dest => dest.Product, opt => opt.Ignore()
            );
        CreateMap<OrderProduct, OrderProductResponse>();
    }
}