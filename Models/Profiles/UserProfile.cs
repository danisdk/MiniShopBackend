using AutoMapper;
using MiniShop.Models.Responses;

namespace MiniShop.Models.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, User>().ForMember(
            dest => dest.Id, opt => opt.Ignore()
        );
        CreateMap<User, UserResponse>();
    }
}