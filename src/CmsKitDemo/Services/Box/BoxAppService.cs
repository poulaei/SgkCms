using CmsKitDemo.Entities;
using CmsKitDemo.Features;
using CmsKitDemo.GlobalFeatures;
using CmsKitDemo.Permissions;
using CmsKitDemo.Services.Boxes;
using CmsKitDemo.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectMapping;
using Volo.CmsKit;
using Volo.CmsKit.Admin;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Permissions;
using Volo.CmsKit.Public;
using Volo.CmsKit.Reactions;
using Volo.CmsKit.Tags;

namespace CmsKitDemo.Services
{
    //[RequiresFeature(CmsKitDemoFeatures.BoxEnable)]
    //[RequiresGlobalFeature(typeof(BoxFeature))]
    public class BoxAppService : CmsKitPublicAppServiceBase
       //ApplicationService
       // CrudAppService<Box, BoxDto, Guid, PagedAndSortedResultRequestDto, CreateBoxDto,UpdateBoxDto>,
       , IBoxAppService
    {
        private readonly IBoxRepository _boxRepository;
        public BoxAppService(IBoxRepository repository, IBoxRepository boxItemRepository) 
        {
            //دسترسی ها برای سرویس های ویرایشی
            //فعلا به منظور سهولت توسعه دهنده فرانت حذف و نادیده گرفته شد
            //CreatePolicyName = CmsKitDemoPermissions.Box.Create;
            //UpdatePolicyName = CmsKitDemoPermissions.Box.Update;
            //DeletePolicyName = CmsKitDemoPermissions.Box.Delete;
            _boxRepository = boxItemRepository;
        }

        public async Task<List<BoxDto>> GetDetailedListAsync()
        {
            //var dbContext = await Repository.GetDbContextAsync();

            //var boxes = await (from box in dbContext.Set<Box>() select box).ToListAsync();

            //return boxes.Select(x => new BoxDto
            //{
            //    Id = x.Id,
            //    Section=x.Section,
            //    Title = x.Title,
            //    Action =x.Action,
            //    ActionUrl=x.ActionUrl,
            //    Summary=x.Summary,
            //    Status=x.Status,
            //    Description = x.Description,
            //    BoxItems = ObjectMapper.Map<List<BoxItem>, List<BoxItemDto>>(x.BoxItems.ToList()),

            //}).ToList();



           

            var boxes = await _boxRepository.GetListAsync(includeDetails:true);

            return ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes);
        }

        public async Task<BoxDto> GetBoxItemsAsync(Guid id)
        {

            var box = await _boxRepository.FindAsync(id,includeDetails: true);

            return ObjectMapper.Map<Box, BoxDto>(box);
        }
    }
}
