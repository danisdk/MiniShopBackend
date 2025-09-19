using AutoMapper;
using MiniShop.Models.Requests;
using MiniShop.Models.Responses;

namespace MiniShop.Models.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, Category>().ForMember(
            dest => dest.Id, opt => opt.Ignore()
        );
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();
    }
}