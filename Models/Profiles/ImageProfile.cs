using AutoMapper;
using MiniShop.Models.Requests;
using MiniShop.Models.Responses;
using MiniShop.Services.Structures;

namespace MiniShop.Models.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, Image>().ForMember(
            dest => dest.Id, opt => opt.Ignore()
        );
        CreateMap<Image, ImageResponse>();
        CreateMap<ImageRequest, Image>();
        CreateMap<FileStructure, Image>();
    }
}