using AutoMapper;
using MiniShop.Models.Responses;

namespace MiniShop.Models.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, Order>().ForMember(
            dest => dest.Id, opt => opt.Ignore()
        );
        CreateMap<Order, OrderResponse>();
    }
}