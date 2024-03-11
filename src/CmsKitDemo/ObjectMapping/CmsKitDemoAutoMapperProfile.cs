using AutoMapper;
using CmsKitDemo.Entities;
using CmsKitDemo.Services.Boxes;
using CmsKitDemo.Services.Dtos;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;
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

        CreateMap<Box, BoxDto>().MapExtraProperties();

        CreateMap<CreateBoxDto, Box>(MemberList.Source).MapExtraProperties();
        CreateMap<UpdateBoxDto, Box>(MemberList.Source).MapExtraProperties();

        CreateMap<BoxItem, BoxItemDto>().ReverseMap();//.MapExtraProperties();

        CreateMap<CreateBoxItemDto, BoxItem>();
        CreateMap<UpdateBoxItemDto, BoxItem>();

    }
}
