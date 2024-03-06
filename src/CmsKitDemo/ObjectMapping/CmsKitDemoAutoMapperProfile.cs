using AutoMapper;
using CmsKitDemo.Entities;
using CmsKitDemo.Services.Dtos;
using Volo.CmsKit.Menus;

namespace CmsKitDemo.ObjectMapping;

public class CmsKitDemoAutoMapperProfile : Profile
{
    public CmsKitDemoAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */

        CreateMap<CreateUpdateGalleryImageDto, GalleryImage>().ReverseMap();

        CreateMap<GalleryImage, GalleryImageDto>().ReverseMap();

        CreateMap<MenuItem, RoyanMenuItemDto>().MapExtraProperties();

    }
}
